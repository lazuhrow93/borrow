using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
using System;

namespace Borrow.Data.Services
{
    public interface IListingService
    {
        public bool CreateListing(PublishListingViewModel p);
        public bool DeactiveListing(IEnumerable<int> listingIds);
        public IEnumerable<Listing> GetNeighborhoodActiveListings(Neighborhood neighborhood);
    }
}
