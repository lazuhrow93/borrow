using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Borrow.Models.Views.TableViews;

namespace Borrow.Data.Services.Implementations
{
    public partial class ProfileService : IProfileService
    {
        public ItemViewModel GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public DeleteItemsViewModel GetDeleteItemsViewModel(AppProfile appProfile)
        {
            var nonListedItems = _itemRepository.Query.Where(i => i.IsListed == false).ToList();
            var parsed = ToViewModel(nonListedItems);
            return new DeleteItemsViewModel()
            {
                ItemViewModels = parsed.Select(s =>
                {
                    return new Models.Views.SelectorViewModel<ItemViewModel>()
                    {
                        IsSelected = false,
                        Entity = s
                    };
                }).ToList()
            };
        }

        public ProfileViewModel GetProfileViewModel(User user)
        {
            var profile = _appProfileRepository.GetById(user.ProfileId);
            var items = _itemRepository.FetchAll().Where(i => i.OwnerId == profile.OwnerId);
            return new ProfileViewModel()
            {
                Username = profile.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Exchanges = 0, // huh?
                OwnerItems = ToViewModel(items).ToList()
            };
        }

        public EditItemViewModel GetEditItemViewModel(int id)
        {
            var currentItem = _itemRepository.GetById(id);
            return new EditItemViewModel()
            {
                ItemId = id,
                NewName = string.Empty,
                NewDescription = string.Empty,
                OwnedSince = currentItem.OwnedSince
            };
        }

        #region Private Helpers

        private IEnumerable<ItemViewModel> ToViewModel(IEnumerable<Item> items)
        {
            var results = new List<ItemViewModel>();
            var itemIds = items.Select(i=> i.Id);
            var ownerIds = items.Select(i => i.OwnerId);
            var dbItems = _itemRepository.Query.Where(i => itemIds.Contains(i.Id));
            var profiles = _appProfileRepository.Query.Where(a => ownerIds.Contains(a.OwnerId));

            var query = dbItems.Join(profiles,
                i => i.OwnerId,
                p => p.OwnerId,
                (i, p) => new
                {
                    i.Id,
                    p.UserName,
                    i.IsListed,
                    i.Name,
                    i.Description,
                    i.OwnedSince,
                    i.Available
                }).ToList();

            return query.Select(q =>
            {
                return new ItemViewModel(q.Id, q.UserName, q.IsListed, q.Name, q.Description, q.OwnedSince.ToString(), q.Available);
            });
            

        }

        #endregion
    }
}
