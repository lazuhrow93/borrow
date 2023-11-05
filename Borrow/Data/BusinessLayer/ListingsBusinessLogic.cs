using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Repositories;
using System;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;
using Azure.Identity;
using NuGet.Packaging;
using MessagePack;
using Borrow.Models.Views.Listings;
using Borrow.Models.Views;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public UserListingsViewModel GetUserListingsViewModel(User user)
        {
            return new UserListingsViewModel()
            {
                ActiveListings = GetUserListings(user, all:false).ToList()
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
        
        public RemoveListingViewModel GetRemoveListingViewModel(User user)
        {
            var userListings = GetUserListings(user, all: false);
            return new RemoveListingViewModel()
            {
                Listings = userListings.Select(l =>
                {
                    return new SelectorViewModel<ListingViewModel>()
                    {
                        IsSelected = false,
                        Entity = l
                    };
                }).ToList()
            };
        }

        public ViewListingViewModel GetViewListingViewModel(int id)
        {
            var listing = ListingsDataLayer.Get(id);
            var appProfile = AppProfileDataLayer.GetByOwnerId(listing.OwnerId);
            var item = ItemDataLayer.Get(listing.ItemId);

            var lvm = Mapper.Map<ListingViewModel>(item);
            Mapper.Map<AppProfile, ListingViewModel>(appProfile, lvm);
            Mapper.Map<Listing, ListingViewModel>(listing, lvm);

            return new ViewListingViewModel()
            {
                ListingViewModel = lvm
            };
        }

        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel(User user)
        {
            var ap = AppProfileDataLayer.Get(user.ProfileId);
            var neighborhood = NeighborhoodDataLayer.Get(ap);
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


            var queryResult = query.Where(q => q.NeighborhoodId.Equals(neighborhood.Id) && q.Active && q.OwnerId != ap.OwnerId);
            var results = new List<ListingViewModel>();

            foreach (var join in queryResult)
            {
                results.Add(new ListingViewModel(join.Id, join.ItemId, join.Name, join.Description, join.DailyRate, join.WeeklyRate, join.UserName, join.OwnerId));
            }

            return new NeighborhoodListingsViewModel()
            {
                Name = neighborhood.Name,
                NeighborhoodListings = results
            };
        }

        #region Helpers

        private IEnumerable<ListingViewModel> GetUserListings(User user, bool all = false)
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

            foreach (var join in queryResult)
            {
                results.Add(new ListingViewModel(join.Id, join.ItemId, join.Name, join.Description, join.DailyRate, join.WeeklyRate, appProfile.UserName, join.OwnerId));
            }

            return results;
        }

        #endregion

    }
}
