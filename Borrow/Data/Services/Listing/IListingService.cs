using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
using System;

namespace Borrow.Data.Services.Listing
{
    public interface IListingService
    {
        public UserListingsViewModel GetUserListingsViewModel();
        public CreateListingViewModel GetCreateListingViewModel();
        public PublishListingViewModel GetPublishListingViewModel();
        public RemoveListingViewModel GetRemoveListingViewModel();
        public ViewListingViewModel GetListingViewModel();
        public NeighborhoodListingsViewModel GetNeighborhoodListingsViewModel();
        public bool CreateListing();
        public bool DeactiveListing(); 
    }
}
