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
        private IMapper _mapper;

        public ListingService(
            IRepository<Listing> listingrepository,
            IRepository<Item> itemRepository,
            IRepository<AppProfile> appProfileRepository,
            IMapper mapper)
        {
            _listingRepository = listingrepository;
            _itemRepository = itemRepository;
            _appProfileRepository = appProfileRepository;
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
            throw new NotImplementedException();
        }

        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel(User user)
        {
            throw new NotImplementedException();
        }

        public PublishListingViewModel GetPublishListingViewModel(int itemId, int profileId)
        {
            throw new NotImplementedException();
        }

        public RemoveListingViewModel GetRemoveListingViewModel(User user)
        {
            throw new NotImplementedException();
        }

        public UserListingsViewModel GetUserListingsViewModel(User user)
        {
            throw new NotImplementedException();
        }
    }
}
