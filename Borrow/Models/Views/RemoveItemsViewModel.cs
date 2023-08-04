using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class RemoveItemsViewModel
    {
        public List<ItemViewModel> Items { get; set; }

        public RemoveItemsViewModel()
        {
            
        }

        public RemoveItemsViewModel(IEnumerable<Item> items)
        {
            var ivms = items.Select(i => { return new ItemViewModel(i); });
            this.Items = ivms.Select(i => { i.IsSelected = false; return i; }).ToList();
        }
    }
}
