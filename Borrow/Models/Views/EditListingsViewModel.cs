using AutoMapper;
using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class EditListingsViewModel
    {
        private readonly IMapper _mapper;
        public List<ItemViewModel> Items { get; set; }
        public Guid Unlist { get; set; }
        public Guid List { get; set; }

        public EditListingsViewModel(IEnumerable<Item> items)
        {
            this.Items = items.Select(i => { return new ItemViewModel(i); }).ToList();
        }
    }
}
