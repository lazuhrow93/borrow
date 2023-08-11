using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Identity;
using System;
using Borrow.Models.Backend;

namespace Borrow.Data.BusinessLayer
{
    public class ListingsBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
        public ListingsDataLayer ListingsDataLayer { get; set; }
        public NeighborhoodDataLayer NeighborhoodDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        public ListingsBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            RequestDataLayer = masterDL.RequestDataLayer;
            NeighborhoodDataLayer = masterDL.NeighborhoodDataLayer;
            ItemDataLayer = masterDL.ItemDataLayer;
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            ListingsDataLayer = masterDL.ListingsDataLayer;
            Mapper = mapper;
        }

        public bool Create(int itemId, decimal dailyRate, decimal weeklyRate)
        {
            var item = ItemDataLayer.Get(itemId);
            ListingsDataLayer.Insert(itemId, item.OwnerId, dailyRate, weeklyRate);
            return true;
        }

        public IEnumerable<Item> GetNeighborhoodListings(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var neighborhood = NeighborhoodDataLayer.Get(appProfile);
            var items = ItemDataLayer.Get(neighborhood);

            return items.Where(i => i.OwnerId.Equals(appProfile.OwnerId) == false);
        }

        public IEnumerable<Item> GetUserListings(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            return ItemDataLayer.GetOwnerItems(appProfile.OwnerId);
        }

        public void InsertItem(User user, List<Item> items)
        {
            var userProfile = AppProfileDataLayer.Get(user.ProfileId);
            for (int index = 0; index < items.Count; ++index)
            {
                items[index].OwnerId = userProfile.OwnerId;
                items[index].NeighborhoodId = userProfile.NeighborhoodId;

            }
            ItemDataLayer.Insert(items);
        }

        public void InsertItem(User user, Item item)
        {
            var userProfile = AppProfileDataLayer.Get(user.ProfileId);
            item.OwnerId = userProfile.OwnerId;
            item.NeighborhoodId = userProfile.NeighborhoodId;

            ItemDataLayer.Insert(item);
        }

        public bool EditItem(User user, int id, string newName, string newDesc, decimal newDailyRate, decimal newWeeklyRate)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var currentItem = ItemDataLayer.Get(id);
            currentItem.Description = newDesc;
            currentItem.Name = newName;
            ItemDataLayer.Update(currentItem);
            return true;
        }

        public bool ChangeListingStatus(User user, int listingId, bool isListed)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var currentItem = ItemDataLayer.Get(listingId);
            if (currentItem is null) return false;
            currentItem.IsListed = isListed;
            ItemDataLayer.Update(currentItem);
            return true;
        }

        public bool RemoveListing(User user, IEnumerable<int> itemIds)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            foreach (var id in itemIds)
            {
                var item = ItemDataLayer.Get(id);
                if (item.OwnerId.Equals(appProfile.OwnerId) == false) return false;
                ItemDataLayer.Delete(id);
            }
            return true;
        }
    }
}
