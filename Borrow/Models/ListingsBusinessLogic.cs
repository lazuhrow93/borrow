using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using System;

namespace Borrow.Models
{
    public class ListingsBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
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
            Mapper = mapper;
        }

        public Item GetItemById(int id)
        {
            var item = ItemDataLayer.Get(id);
            if (item is not null) return item;
            else throw new Exception($"Unable to find the item with id {id}");
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
            for(int index = 0; index < items.Count; ++index) 
            {
                items[index].UserName = user.UserName;
                items[index].OwnerId = userProfile.OwnerId;
                items[index].NeighborhoodId = userProfile.NeighborhoodId;
                items[index].Identifier = Guid.NewGuid();
                
            }
            ItemDataLayer.Insert(items);
        }

        public void InsertItem(User user, Item item)
        {
            var userProfile = AppProfileDataLayer.Get(user.ProfileId);
            item.UserName = user.UserName;
            item.OwnerId = userProfile.OwnerId;
            item.NeighborhoodId = userProfile.NeighborhoodId;
            item.Identifier = Guid.NewGuid();

            ItemDataLayer.Insert(item);
        }

        public bool EditItem(User user, Item New)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var currentItem = ItemDataLayer.Get(New.Id);
            if (currentItem is null) return false;
            currentItem.WeeklyRate = New.WeeklyRate;
            currentItem.DailyRate = New.DailyRate;
            currentItem.Available = New.Available;
            currentItem.Description = New.Description;
            currentItem.Name = New.Name;
            currentItem.IsListed = New.IsListed;
            ItemDataLayer.Update(currentItem);
            return true;
        }

        public bool ChangeListingStatus(User user, int listingId, bool isListed)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var currentItem = ItemDataLayer.Get(listingId);
            if(currentItem is null) return false;
            currentItem.IsListed = isListed;
            ItemDataLayer.Update(currentItem);
            return true;
        }

        public bool RemoveListing(User user, IEnumerable<int> itemIds)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            foreach(var id in itemIds)
            {
                var item = ItemDataLayer.Get(id);
                if (item.OwnerId.Equals(appProfile.OwnerId) == false) return false;
                ItemDataLayer.Delete(id);
            }
            return true;
        }
    }
}
