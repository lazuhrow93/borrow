using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Borrow.Models.Views.TableViews;
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

        public IEnumerable<ItemViewModel> GetUserItems(User user)
        {
            var setofItems = Enumerable.Empty<ItemViewModel>();
            var ap = AppProfileDataLayer.Get(user.ProfileId);
            var items = ItemDataLayer.GetOwnerItems(ap.OwnerId);
            setofItems = Mapper.Map<IEnumerable<ItemViewModel>>(items);

            return setofItems;
        }

        public ItemViewModel GetItem(int id)
        {
            var item = ItemDataLayer.Get(id);
            var parsedItem = Mapper.Map<ItemViewModel>(item);
            return parsedItem;
        }

        public bool EditItem(EditItemViewModel NewDetails)
        {
            var updatedItem = Mapper.Map<Item>(NewDetails);
            ItemDataLayer.Update(updatedItem);
            return true;
        }

        public bool CreateItemsForUser(User user, AddItemViewModel newItemInfo)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var itemsToAdd = new List<Item>();
            foreach(var newItemViewModel in newItemInfo.ItemsToSave)
            {
                var newItem = Mapper.Map<Item>(newItemViewModel);
                newItem.Available = false;
                newItem.OwnerId = profile.OwnerId;
                newItem.IsListed = false;
                newItem.NeighborhoodId = profile.NeighborhoodId;
                itemsToAdd.Add(newItem);
            }
            ItemDataLayer.Insert(itemsToAdd);
            return true;
        }

        internal void DeleteItem(User user, IEnumerable<int> enumerable)
        {
            throw new NotImplementedException();
        }
    }
}
