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

        public UserListingsViewModel(IEnumerable<ListingViewModel> listings)
        {
            ListedItems = listings.ToList();
        }
    }
}
