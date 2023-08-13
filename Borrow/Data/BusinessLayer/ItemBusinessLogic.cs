using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
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

        public IEnumerable<Item> CreateItemForUser(User user, List<Item> items)
        {
            var userProfile = AppProfileDataLayer.Get(user.ProfileId);
            for (int index = 0; index < items.Count; ++index)
            {
                items[index].OwnerId = userProfile.OwnerId;
                items[index].NeighborhoodId = userProfile.NeighborhoodId;

            }
            ItemDataLayer.Insert(items);
            return items;
        }

        public Item EditItem(int id, string newName, string newDesc, decimal newDailyRate, decimal newWeeklyRate)
        {
            var currentItem = ItemDataLayer.Get(id);
            currentItem.Description = newDesc;
            currentItem.Name = newName;
            ItemDataLayer.Update(currentItem);
            return currentItem;
        }

        public IEnumerable<Item> GetUserItems(User user)
        {
            var ap = AppProfileDataLayer.Get(user.ProfileId);
            return ItemDataLayer.GetOwnerItems(ap.OwnerId);
        }
    }
}
