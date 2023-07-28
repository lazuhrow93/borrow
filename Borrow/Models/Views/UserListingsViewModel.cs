using AutoMapper;
using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class UserListingsViewModel
    {
        public List<ItemViewModel> ListedItems { get; set; }
        public List<ItemViewModel> NonListedItems { get; set; }

        public UserListingsViewModel(IMapper mapper, IEnumerable<Item> items)
        {
            ListedItems = new();
            NonListedItems = new();
            foreach (var item in items)
            {
                if (item.IsListed) ListedItems.Add(new ItemViewModel(item));
                else NonListedItems.Add(new ItemViewModel(item));
            }
        }
    }
}
