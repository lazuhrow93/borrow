using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using System.Security.Claims;

namespace Borrow.Data.BusinessLayer
{

    public class ItemBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
        public NeighborhoodDataLayer NeighborhoodDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        public ItemBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            RequestDataLayer = masterDL.RequestDataLayer;
            ItemDataLayer = masterDL.ItemDataLayer;
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            Mapper = mapper;
        }

        public Item? GetItem(int id)
        {
            return ItemDataLayer.Get(id);
        }

        public IEnumerable<Item> GetAvailableItems(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var items = ItemDataLayer.GetOwnerItems(appProfile.OwnerId);
            return items.Where(i => i.Available);
        }
    }
}
