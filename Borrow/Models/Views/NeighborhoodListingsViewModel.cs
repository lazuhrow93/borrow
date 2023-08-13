using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class NeighborhoodListingsViewModel
    {
        public string Name { get; set; }
        public List<ListingViewModel> NeighborhoodListings { get; set; }

        public NeighborhoodListingsViewModel()
        {
            
        }

        public NeighborhoodListingsViewModel(IEnumerable<ListingViewModel> lvm, string name)
        {
            Name = name;
            NeighborhoodListings = lvm.ToList();
        }
    }
}
