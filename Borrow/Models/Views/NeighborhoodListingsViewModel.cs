using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class NeighborhoodListingsViewModel
    {
        public string Name { get; set; }
        public List<ItemViewModel> NeighborhoodListings { get; set; }
        public List<ItemViewModel> NeighborhoodUnlisted { get; set; }

        public NeighborhoodListingsViewModel()
        {
            
        }

        public NeighborhoodListingsViewModel(IEnumerable<Item> neighborhoodItems, string neighborhoodName)
        {
            var listed = neighborhoodItems.Where(i => i.IsListed == true);
            var unlisted = neighborhoodItems.Where(i => i.IsListed == false);

            NeighborhoodListings = new();
            NeighborhoodUnlisted = new();

            foreach(var item in listed)
            {
                NeighborhoodListings.Add(new ItemViewModel(item));
            }

            foreach (var item in unlisted)
            {
                NeighborhoodUnlisted.Add(new ItemViewModel(item));
            }
            Name = neighborhoodName;
        }
    }
}
