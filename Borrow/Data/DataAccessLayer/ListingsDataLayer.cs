using Borrow.Models.Backend;

namespace Borrow.Data.DataAccessLayer
{
    public class ListingsDataLayer
    {
        public BorrowContext Context { get; set; }

        public ListingsDataLayer(BorrowContext borrowContext)
        {
            Context = borrowContext;
        }

        public bool Insert(int itemId, int ownerId, decimal dailyrate, decimal weeklyrate)
        {
            Context.Add(new Listing()
            {
                ItemId = itemId,
                DailyRate = dailyrate,
                WeeklyRate = weeklyrate,
                OwnerId = ownerId
            });
            Context.SaveChanges();
            return true;
        }

    }
}
