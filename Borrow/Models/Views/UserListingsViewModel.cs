using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class UserListingsViewModel
    {
        public List<ListingViewModel> ListedItems { get; set; }

        public UserListingsViewModel()
        {
            
        }

        public UserListingsViewModel(IEnumerable<Backend.Listing> listings)
        {
            ListedItems = listings.Select(l =>
            {
                return new ListingViewModel(l);
            }).ToList();
        }
    }
}
