using Borrow.Data.Repositories.Implementations;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Data.Services.Implementations
{
    public partial class ListingService : IListingService
    {
        public CreateListingViewModel GetCreateListingViewModel(User user)
        {
            var profile = _appProfileRepository.GetById(user.ProfileId);
            var items = _itemRepository.Query.
                Where(i => i.OwnerId == profile.OwnerId).
                Where(i => i.IsListed == false).ToList();
            return new CreateListingViewModel()
            {
                AvailableItems = _mapper.Map<List<ItemViewModel>>(items)
            };
        }

        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel(User user)
        {
            var ap = _appProfileRepository.GetById(user.ProfileId);
            var neighborhood = _neighborhoodRepository.GetById(ap.NeighborhoodId);
            var items = _itemRepository.FetchAll();
            var listings = _listingRepository.FetchAll();
            var appProfiles = _appProfileRepository.FetchAll();

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


            var queryResult = query.Where(q => q.NeighborhoodId.Equals(neighborhood.Id) && q.Active && q.OwnerId != ap.OwnerId).ToList();
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

        public ViewListingViewModel GetViewListingViewModel(int id)
        {
            var listing = _listingRepository.GetById(id);
            var appProfile = _appProfileRepository.Query.First(a => a.OwnerId == listing.OwnerId);
            var item = _itemRepository.GetById(listing.ItemId);

            var lvm = _mapper.Map<ListingViewModel>(item);
            _mapper.Map<AppProfile, ListingViewModel>(appProfile, lvm);
            _mapper.Map<Listing, ListingViewModel>(listing, lvm);

            return new ViewListingViewModel()
            {
                ListingViewModel = lvm
            };
        }

        public UserListingsViewModel GetUserListingsViewModel(User user)
        {
            return new UserListingsViewModel()
            {
                ActiveListings = GetUserListings(user, all: false).ToList()
            };

        }

        public PublishListingViewModel GetPublishListingViewModel(int itemId, int profileId)
        {
            var appProfile = _appProfileRepository.GetById(profileId);
            var item = _itemRepository.GetById(itemId);
            return new PublishListingViewModel()
            {
                ItemInfo = _mapper.Map<ItemViewModel>(item),
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

        #region Helpers

        private IEnumerable<ListingViewModel> GetUserListings(User user, bool all = false)
        {
            var appProfile = _appProfileRepository.GetById(user.ProfileId);
            var items = _itemRepository.FetchAll().ToList();
            var listings = _listingRepository.FetchAll().ToList();


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

            var queryResult =
                (all) ?
                query.Where(q => q.OwnerId.Equals(appProfile.OwnerId)).ToList()
                : query.Where(q => q.OwnerId.Equals(appProfile.OwnerId) && q.Active).ToList();

            var results = new List<ListingViewModel>();

            foreach (var join in queryResult)
            {
                results.Add(new ListingViewModel(join.Id, join.ItemId, join.Name, join.Description, join.DailyRate, join.WeeklyRate, appProfile.UserName, join.OwnerId));
            }

            return results;
        }

        public IEnumerable<Listing> GetNeighborhoodActiveListings(Neighborhood neighborhood)
        {
            return _listingRepository.Query.Where(l => l.NeighborhoodId == neighborhood.Id);
        }

        #endregion
    }
}
