namespace Borrow.Data.DataAccessLayer.Interfaces
{
    public interface IMasterDL
    {
        public NeighborhoodDataLayer NeighborhoodDataLayer { get; set; }
        public RequestDataLayer RequestDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }

    }
}
