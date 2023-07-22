using AutoMapper;
using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class NeighborhoodListingsViewModel
    {
        private IMapper _mapper;

        public string Name { get; set; }
        public List<ItemViewModel> NeighborhoodListings { get; set; }
        public List<ItemViewModel> NeighborhoodUnlisted { get; set; }

        public NeighborhoodListingsViewModel()
        {
            
        }

        public NeighborhoodListingsViewModel(IMapper mapper, IEnumerable<Item> neighborhoodItems, string name)
        {
            _mapper = mapper;
            var listed = neighborhoodItems.Where(i => i.IsListed == true);
            var unlisted = neighborhoodItems.Where(i => i.IsListed == false);

            NeighborhoodListings = _mapper.Map<List<ItemViewModel>>(listed);
            NeighborhoodUnlisted = _mapper.Map<List<ItemViewModel>>(unlisted);
            Name = name;
        }
    }
}
