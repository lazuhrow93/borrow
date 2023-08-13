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

        public NeighborhoodListingsViewModel(IEnumerable<Backend.Listing> rawlistings, string neighborhoodName)
        {
            NeighborhoodListings = rawlistings.Select(l =>
            {
                return new ListingViewModel(l);
            }).ToList();
            Name = neighborhoodName;
        }
    }
}
