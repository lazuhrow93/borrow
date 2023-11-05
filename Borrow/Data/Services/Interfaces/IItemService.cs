using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Borrow.Models.Views.TableViews;
using System;

namespace Borrow.Data.Services
{
    public interface IItemService
    {
        public IEnumerable<ItemViewModel> GetUserItems(User user);
        public ItemViewModel GetItem(int id);
        public bool EditItem(EditItemViewModel viewModel);
        public bool CreateItems(User user, AddItemViewModel viewModel);
        public bool DeleteItem(User user, int ids);
        public bool DeleteItems(User user, IEnumerable<int> ids);

    }
}