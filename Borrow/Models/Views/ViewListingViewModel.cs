using AutoMapper;
using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class ViewListingViewModel
    {
        private IMapper _mapper;
        private Item? item;
        public ItemViewModel ItemViewModel { get; set; }

        public ViewListingViewModel()
        {
            
        }

        public ViewListingViewModel(IMapper mapper, Item? item)
        {
            this._mapper = mapper;
            this.item = item;
            this.ItemViewModel = _mapper.Map<ItemViewModel>(item);
        }
    }
}
