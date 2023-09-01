using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class ViewListingViewModel
    {
        public ListingViewModel ListingViewModel { get; set; }

        public ViewListingViewModel()
        {

        }

        public ViewListingViewModel(ListingViewModel listingViewModel)
        {
            ListingViewModel = listingViewModel;
        }
    }
}
