using AutoMapper;
using Borrow.Models.Listings;

namespace Borrow.Models.Views
{
    public class EditListingsViewModel
    {
        private readonly IMapper _mapper;
        public List<ItemViewModel> Items { get; set; }

        public EditListingsViewModel()
        {
            Items = new();
        }
    }
}
