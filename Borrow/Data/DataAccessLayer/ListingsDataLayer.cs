using Borrow.Models.Backend;
using System;

namespace Borrow.Data.DataAccessLayer
{
    public class ListingsDataLayer : Datalayer
    {
        public ListingsDataLayer(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;
        }

        public Listing Insert(int itemId, int ownerId, decimal dailyrate, decimal weeklyrate, int neighborhoodId, bool IsActive)
        {
            var newItem = new Listing()
            {
                ItemId = itemId,
                DailyRate = dailyrate,
                WeeklyRate = weeklyrate,
                OwnerId = ownerId,
                NeighborhoodId = neighborhoodId,
                Active = IsActive
            };

            BorrowContext.Add(newItem);
            BorrowContext.SaveChanges();
            return newItem;
        }

        public IEnumerable<Listing> GetNeighborhoodListings(int neighborhood)
        {
            return BorrowContext.Listing.Where(l => l.NeighborhoodId == neighborhood);
        }

        public IEnumerable<Listing> GetOwnerListings(int owner)
        {
            return BorrowContext.Listing.Where(l => l.OwnerId.Equals(owner));
        }

        public Listing? Get(int id)
        {
            return BorrowContext.Listing.Where(l => l.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Listing> Get(IEnumerable<int> ids)
        {
            List<Listing> listings = new List<Listing>();
            foreach (var id in ids)
            {
                listings.Add(BorrowContext.Listing.Find(id));
            }
            return listings;
        }

        public bool Update(IEnumerable<Listing> listings)
        {
            foreach (var listing in listings)
            {
                var currentListing = BorrowContext.Listing.Where(l => l.Id.Equals(listing.Id)).FirstOrDefault();
                currentListing = listing;
                BorrowContext.SaveChanges();
            }
            return true;
        }
    }
}
