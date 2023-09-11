using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;

namespace Borrow.Models.Views
{
    public class UserBorrowRequestsViewModel
    {
        public List<RequestViewModel> Outgoing { get; set; }
        public List<RequestViewModel> Incoming { get; set; }

        public UserBorrowRequestsViewModel()
        {
            
        }

        public UserBorrowRequestsViewModel(IEnumerable<(Request Request, Item Item)> outGoing, IEnumerable<(Request Request, Item Item)> inComing)
        {
        }
    }
}
