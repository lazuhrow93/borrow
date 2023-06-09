using AutoMapper;
using Borrow.Models.Listings;

namespace Borrow.Models.Views
{
    public class EditListingsViewModel
    {
        private readonly IMapper _mapper;
        public List<ItemViewModel> Items { get; set; }
        public Guid Unlist { get; set; }
        public Guid List { get; set; }

        public EditListingsViewModel()
        {

        }

        public EditListingsViewModel(IMapper mapper)
        {
            _mapper = mapper;
            Items = new();
        }

        public void MapItems(List<Item> items)
        {
            this.Items = _mapper.Map<List<ItemViewModel>>(items);
        }
    }
}
