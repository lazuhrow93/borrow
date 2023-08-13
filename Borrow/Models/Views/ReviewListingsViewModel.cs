using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class ReviewListingsViewModel
    {
        public List<ItemViewModel> Items { get; set; }

        public ReviewListingsViewModel()
        {
            
        }

        public ReviewListingsViewModel(IEnumerable<Item> items)
        {
            var ivms = items.Select(i => { return new ItemViewModel(i); });
            this.Items = ivms.Select(i => { i.IsSelected = false; return i; }).ToList();
        }
    }
}
