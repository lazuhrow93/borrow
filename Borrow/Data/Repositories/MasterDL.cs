using Borrow.Data.Repositories.Interfaces;

namespace Borrow.Data.Repositories
{
    public class MasterDL : IMasterDL
    {
        public NeighborhoodDataLayer NeighborhoodDataLayer { get; set; }
        public RequestDataLayer RequestDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        public ListingsDataLayer ListingsDataLayer { get; set; }


        public MasterDL(BorrowContext borrowContext)
        {
            NeighborhoodDataLayer = new(borrowContext);    
            RequestDataLayer = new RequestDataLayer(borrowContext);
            ItemDataLayer = new ItemDataLayer(borrowContext);
            AppProfileDataLayer = new AppProfileDataLayer(borrowContext);
            ListingsDataLayer = new ListingsDataLayer(borrowContext);
        }
    }
}
