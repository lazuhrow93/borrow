using Borrow.Data.DataAccessLayer.Interfaces;

namespace Borrow.Data.DataAccessLayer
{
    public class MasterDL : IMasterDL
    {
        public NeighborhoodDL NeighborhoodDL { get; set; }
        public RequestDL RequestDL { get; set; }

        public MasterDL(BorrowContext borrowContext)
        {
            NeighborhoodDL = new(borrowContext);    
            RequestDL = new RequestDL(borrowContext);
        }
    }
}
