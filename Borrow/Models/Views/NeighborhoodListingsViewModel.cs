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
    }
}
