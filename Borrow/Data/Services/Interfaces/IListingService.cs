using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
using System;

namespace Borrow.Data.Services
{
    public interface IListingService
    {
        public UserListingsViewModel GetUserListingsViewModel(User user);
        public CreateListingViewModel GetCreateListingViewModel(User user);
        public PublishListingViewModel GetPublishListingViewModel(int itemId, int profileId);
        public RemoveListingViewModel GetRemoveListingViewModel(User user);
        public ViewListingViewModel GetViewListingViewModel(int id);
        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel(User user);
        public bool CreateListing(PublishListingViewModel p);
        public bool DeactiveListing(IEnumerable<int> listingIds); 
    }
}
