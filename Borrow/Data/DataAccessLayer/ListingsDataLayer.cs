using Borrow.Models.Backend;

namespace Borrow.Data.DataAccessLayer
{
    public class ListingsDataLayer : Datalayer
    {
        public BorrowContext Context { get; set; }

        public ListingsDataLayer(BorrowContext borrowContext)
        {
            Context = borrowContext;
        }

        public Listing Insert(int itemId, int ownerId, decimal dailyrate, decimal weeklyrate)
        {
            var newItem = new Listing()
            {
                ItemId = itemId,
                DailyRate = dailyrate,
                WeeklyRate = weeklyrate,
                OwnerId = ownerId
            };

            Context.Add(newItem);
            Context.SaveChanges();
            return newItem;
        }

        public IEnumerable<Listing> GetNeighborhoodListings(int neighborhood)
        {
            return Context.Listing.Where(l => l.NeighborhoodId == neighborhood);
        }

        public IEnumerable<Listing> GetOwnerListings(int owner)
        {
            return Context.Listing.Where(l => l.OwnerId.Equals(owner));
        }
    }
}
