using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Identity;
using System;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;
using Azure.Identity;
using NuGet.Packaging;
using MessagePack;

namespace Borrow.Data.BusinessLayer
{
    public class ListingsBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
        public ListingsDataLayer ListingsDataLayer { get; set; }
        public NeighborhoodDataLayer NeighborhoodDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        public ListingsBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            RequestDataLayer = masterDL.RequestDataLayer;
            NeighborhoodDataLayer = masterDL.NeighborhoodDataLayer;
            ItemDataLayer = masterDL.ItemDataLayer;
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            ListingsDataLayer = masterDL.ListingsDataLayer;
            Mapper = mapper;
        }

        public bool Create(int itemId, decimal dailyRate, decimal weeklyRate)
        {
            var item = ItemDataLayer.Get(itemId);
            ListingsDataLayer.Insert(itemId, item.OwnerId, dailyRate, weeklyRate);
            return true;
        }

        public bool RemoveListing(User user, IEnumerable<int> itemIds)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            foreach (var id in itemIds)
            {
                var item = ItemDataLayer.Get(id);
                if (item.OwnerId.Equals(appProfile.OwnerId) == false) return false;
                ItemDataLayer.Delete(id);
            }
            return true;
        }

        public ListingViewModel GetListing(int id)
        {
            var listing = ListingsDataLayer.Get(id);
            var appProfile = AppProfileDataLayer.GetByOwnerId(listing.OwnerId);
            var item = ItemDataLayer.Get(listing.ItemId);

            return new ListingViewModel(listing.Id, listing.ItemId, item.Name, item.Description, listing.DailyRate, listing.WeeklyRate, appProfile.UserName, listing.OwnerId);
        }

        public IEnumerable<ListingViewModel> GetUserListings(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var username = appProfile.UserName;
            var bc = ListingsDataLayer.BorrowContext;
            var items = bc.Item;
            var listings = bc.Listing;

            var query = listings.Join(items,
                l => l.ItemId,
                i => i.Id,
                (l, i) => new
                {
                    l.Id,
                    l.ItemId,
                    l.DailyRate,
                    l.WeeklyRate,
                    l.OwnerId,
                    i.Name,
                    i.Description
                });

            var queryResult = query.Where(q => q.OwnerId.Equals(appProfile.OwnerId));
            var results = new List<ListingViewModel>();

            foreach(var join in query)
            {
                results.Add(new ListingViewModel(join.Id, join.ItemId, join.Name, join.Description, join.DailyRate, join.WeeklyRate, appProfile.UserName, join.OwnerId));
            }

            return results;
        }

        public IEnumerable<ListingViewModel> GetNeighborhoodListings(Neighborhood neighborhood)
        {
            var bc = ListingsDataLayer.BorrowContext;
            var items = bc.Item;
            var listings = bc.Listing;
            var appProfiles = bc.AppProfile;

            var query = listings.Join(items,
                l => l.ItemId,
                i => i.Id,
                (l, i) => new
                {
                    l.Id,
                    l.ItemId,
                    l.DailyRate,
                    l.WeeklyRate,
                    l.OwnerId,
                    i.Name,
                    i.Description,
                    l.NeighborhoodId
                }).Join(appProfiles,
                li => li.OwnerId,
                p => p.OwnerId,
                (li, p) => new
                {
                    li.Id,
                    li.ItemId,
                    li.DailyRate,
                    li.WeeklyRate,
                    li.OwnerId,
                    li.Name,
                    li.Description,
                    li.NeighborhoodId,
                    p.UserName,
                });


            var queryResult = query.Where(q => q.NeighborhoodId.Equals(neighborhood.Id));
            var results = new List<ListingViewModel>();

            foreach (var join in query)
            {
                results.Add(new ListingViewModel(join.Id, join.ItemId, join.Name, join.Description, join.DailyRate, join.WeeklyRate, join.UserName, join.OwnerId));
            }

            return results;

        }
    }
}
