using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class UserListingsViewModel
    {
        public List<ListingViewModel> ActiveListings { get; set; }

        public UserListingsViewModel()
        {
            
        }
    }
}
