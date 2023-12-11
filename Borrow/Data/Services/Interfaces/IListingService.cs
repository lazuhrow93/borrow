using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
using System;

namespace Borrow.Data.Services.Interfaces
{
    public interface IListingService
    {
        public CreateListingViewModel GetCreateListingViewModel(User user);
        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel(User user);
        public ViewListingViewModel GetViewListingViewModel(int id);
        public UserListingsViewModel GetUserListingsViewModel(User user);
        public PublishListingViewModel GetPublishListingViewModel(int itemId, int profileId);
        public RemoveListingViewModel GetRemoveListingViewModel(User user);
        public void CreateListing(PublishListingViewModel viewModel);
        public void DeactivateListing(IEnumerable<int> listingIds);
    }
}
