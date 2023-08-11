using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views.Listing
{
    public class CreateListingViewModel
    {
        public List<ItemViewModel> AvailableItems { get; set; }

        public CreateListingViewModel() { }

        public CreateListingViewModel(IEnumerable<Item> item)
        {
            AvailableItems = item.Select(i =>
            {
                return new ItemViewModel(i);
            }).ToList();
        }
    }
}
