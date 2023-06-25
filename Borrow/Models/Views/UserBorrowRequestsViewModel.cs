using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class UserBorrowRequestsViewModel
    {
        private IMapper _mapper { get; set; }
        public List<BorrowRequestViewModel> Outgoing { get; set; }
        public List<BorrowRequestViewModel> Incoming { get; set; }
    }
}
