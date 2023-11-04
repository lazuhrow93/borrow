using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
using Borrow.Models.Backend;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Repositories;

namespace Borrow.Data.Services
{
    public class ListingService : IListingService
    {
        private IRepository<Listing> _listingRepository;
        private IRepository<Item> _itemRepository;

        public ListingService(IRepository<Listing> listingrepository,
            IRepository<Item> itemRepository)
        {
            _listingRepository = listingrepository;
            _itemRepository = itemRepository;
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
            throw new NotImplementedException();
        }

        public CreateListingViewModel GetCreateListingViewModel()
        {
            throw new NotImplementedException();
        }

        public ViewListingViewModel GetListingViewModel()
        {
            throw new NotImplementedException();
        }

        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel()
        {
            throw new NotImplementedException();
        }

        public PublishListingViewModel GetPublishListingViewModel()
        {
            throw new NotImplementedException();
        }

        public RemoveListingViewModel GetRemoveListingViewModel()
        {
            throw new NotImplementedException();
        }

        public UserListingsViewModel GetUserListingsViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
