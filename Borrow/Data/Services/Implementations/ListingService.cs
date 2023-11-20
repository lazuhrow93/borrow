using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
using Borrow.Models.Backend;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Repositories;
using AutoMapper;
using Borrow.Models.Views.TableViews;

namespace Borrow.Data.Services
{
    public class ListingService : IListingService
    {
        private IRepository<Listing> _listingRepository;
        private IRepository<Item> _itemRepository;
        private IRepository<AppProfile> _appProfileRepository;
        private IRepository<Neighborhood> _neighborhoodRepository;
        private IMapper _mapper;

        public ListingService(
            IRepository<Listing> listingrepository,
            IRepository<Item> itemRepository,
            IRepository<AppProfile> appProfileRepository,
            IRepository<Neighborhood> neighborhoodRepository,
            IMapper mapper)
        {
            _listingRepository = listingrepository;
            _itemRepository = itemRepository;
            _appProfileRepository = appProfileRepository;
            _neighborhoodRepository = neighborhoodRepository;
            _mapper = mapper;
        }

        public bool CreateListing(PublishListingViewModel viewModel)
        {
            var item = _itemRepository.GetById(viewModel.ItemInfo.ItemId);
            item.IsListed = true;
            _itemRepository.Save();
            var newItem = new Listing()
            {
                ItemId = item.Id,
                DailyRate = viewModel.DailyRate,
                WeeklyRate = viewModel.WeeklyRate,
                OwnerId = item.OwnerId,
                NeighborhoodId = viewModel.NeighborhoodId,
                Active = true
            };
            _listingRepository.Add(newItem);
            _listingRepository.Save();
            return true;
        }

        public bool DeactiveListing(IEnumerable<int> listingIds)
        {
            foreach(var id in listingIds)
            {
                var listing = _listingRepository.GetById(id);
                listing.Active = false;
                var item = _itemRepository.GetById(listing.ItemId);
                item.IsListed = false;
            }
            _listingRepository.Save();
            _itemRepository.Save(); //do i need to do the second one
            return true;
        }

        public CreateListingViewModel GetCreateListingViewModel(User user)
        {
            //need to get all ITems that are not listed
            var approfile = _appProfileRepository.GetById(user.ProfileId);
            var unlistedItems = 
                _itemRepository.Query.
                Where(i => i.OwnerId == approfile.OwnerId).
                Where(i => i.IsListed == false).ToList();
            var setOfItems = _mapper.Map<List<ItemViewModel>>(unlistedItems);
            return new CreateListingViewModel()
            {
                AvailableItems = setOfItems
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

        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel(User user)
        {
            var ap = _appProfileRepository.GetById(user.ProfileId);
            var neighborhood = _neighborhoodRepository.GetById(ap.NeighborhoodId);
            var items = _itemRepository.FetchAll();
            var appProfiles = _appProfileRepository.FetchAll();

            var query = _listingRepository.Query.Join(items,
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

        public UserListingsViewModel GetUserListingsViewModel(User user)
        {
            return new UserListingsViewModel()
            {
                ActiveListings = GetUserListings(user, all: false).ToList()
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

        #endregion
    }
}
