using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Listings;

namespace Borrow.Data.Services.Implementations
{
    public partial class ListingControllerService : IListingControllerService
    {
        protected readonly IRepository<Item> _itemRepository;
        protected readonly IRepository<AppProfile> _appProfileRepository;
        protected readonly IRepository<Neighborhood> _neighborhoodRepository;
        protected readonly IRepository<Listing> _listingRepository;
        protected readonly IMapper _mapper;

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

        public bool DeactivateListing(IEnumerable<int> listingIds)
        {
            foreach (var id in listingIds)
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
    }
}

