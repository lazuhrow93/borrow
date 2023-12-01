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
