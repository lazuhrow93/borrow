using Borrow.Models.Backend;
using Borrow.Models.Identity;

namespace Borrow.Data.DataAccessLayer
{
    public class NeighborhoodDataLayer : Datalayer
    {
        private BorrowContext BorrowContext { get; set; }

        public NeighborhoodDataLayer(BorrowContext borrowContext)
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

        public Neighborhood? Get(AppProfile profile)
        {
            return BorrowContext.Neighborhood.Where(n=>n.Id.Equals(profile.NeighborhoodId)).FirstOrDefault();
        }
    }
}
