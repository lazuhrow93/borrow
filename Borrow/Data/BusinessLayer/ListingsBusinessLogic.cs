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
using Borrow.Models.Views.Listings;

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

        public CreateListingViewModel GetCreateListingViewModel(User user)
        {
            //need to get all ITems that are not listed
            var approfile = AppProfileDataLayer.Get(user.ProfileId);
            var unlistedItems = ItemDataLayer.GetUnlisted(approfile.OwnerId);
            var setOfItems = Mapper.Map<List<ItemViewModel>>(unlistedItems);
            return new CreateListingViewModel()
            {
                AvailableItems = setOfItems
            };
        }

        public PublishListingViewModel GetPublishListingViewModel(int itemId, int profileId)
        {
            var appProfile = AppProfileDataLayer.Get(profileId);
            var item = ItemDataLayer.Get(itemId);
            return new PublishListingViewModel()
            {
                ItemInfo = Mapper.Map<ItemViewModel>(item),
                DailyRate = 0.0M,
                WeeklyRate = 0.0M,
                NeighborhoodId = appProfile.NeighborhoodId
            };
        }

        public bool Create(PublishListingViewModel p)
        {
            var item = ItemDataLayer.Get(p.ItemInfo.ItemId);
            item.IsListed = true;
            ItemDataLayer.Update(item);
            ListingsDataLayer.Insert(p.ItemInfo.ItemId, item.OwnerId, p.DailyRate, p.WeeklyRate, p.NeighborhoodId, true);
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

        public IEnumerable<ListingViewModel> GetUserListings(User user, bool all = false)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var username = appProfile.UserName;
            var ownerId = appProfile.OwnerId;
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
                    i.Description,
                    l.Active
                });
            var queryResult = (all) ? query.Where(q => q.OwnerId.Equals(ownerId)) : query.Where(q => q.OwnerId.Equals(ownerId) && q.Active);
            var results = new List<ListingViewModel>();

            foreach(var join in queryResult)
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
                    l.NeighborhoodId,
                    l.Active
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
                    li.Active
                });


            var queryResult = query.Where(q => q.NeighborhoodId.Equals(neighborhood.Id) && q.Active);
            var results = new List<ListingViewModel>();

            foreach (var join in queryResult)
            {
                results.Add(new ListingViewModel(join.Id, join.ItemId, join.Name, join.Description, join.DailyRate, join.WeeklyRate, join.UserName, join.OwnerId));
            }

            return results;

        }

        public bool DeactiveListing(IEnumerable<int> listingIds)
        {
            var listings = ListingsDataLayer.Get(listingIds).ToList();
            var items = ItemDataLayer.Get(listings.Select(l => l.ItemId)).ToList();

            for(int i = 0; i <listings.Count; ++i)
            {
                listings[i].Active = false;
                
            }
            for (int i = 0; i < items.Count(); ++i)
            {
                items[i].IsListed = false;
            }
            return (ListingsDataLayer.Update(listings) && ItemDataLayer.Update(items));
        }
    }
}
