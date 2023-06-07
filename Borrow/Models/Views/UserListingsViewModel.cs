using AutoMapper;
using Borrow.Models.Listings;

namespace Borrow.Models.Views
{
    public class UserListingsViewModel
    {
        private readonly IMapper _mapper;
        public List<ItemViewModel> ListedItems { get; set; }
        public List<ItemViewModel> NonListedItems { get; set; }

        public UserListingsViewModel(IMapper mapper)
        {
            this._mapper = mapper;
            ListedItems = new();
            NonListedItems = new();
        }

        public void MapItems(List<Item> items)
        {
            foreach(var item in items)
            {
                if (item.IsListed) ListedItems.Add(_mapper.Map<ItemViewModel>(item));
                else NonListedItems.Add(_mapper.Map<ItemViewModel>(item));
                
            }
        }
    
    }
}
