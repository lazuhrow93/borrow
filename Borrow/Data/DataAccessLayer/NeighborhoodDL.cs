using Borrow.Models.Backend;

namespace Borrow.Data.DataAccessLayer
{
    public class NeighborhoodDL
    {
        private BorrowContext BorrowContext { get; set; }

        public NeighborhoodDL(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;
        }

        public bool Exist(Neighborhood neighborhood)
        {
            return BorrowContext.Neighborhood.Where(n => n.Id.Equals(neighborhood.Id)).Count() > 0;
        }

        public bool Create(Neighborhood neighborhood)
        {
            BorrowContext.Add(neighborhood);
            BorrowContext.SaveChanges();
            return true;
        }

        public Neighborhood? Get(Neighborhood neighborhood)
        {
            return BorrowContext.Neighborhood.Where(n => n.Id.Equals(neighborhood.Id)).FirstOrDefault();
        }
    }
}
