using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views
{
    public class UserBorrowRequestsViewModel
    {
        private IMapper _mapper { get; set; }
        public List<RequestViewModel> Outgoing { get; set; }
        public List<RequestViewModel> Incoming { get; set; }

        public UserBorrowRequestsViewModel()
        {
            
        }

        public UserBorrowRequestsViewModel(IMapper mapper, IEnumerable<(Request Request, Item Item)> outGoing, IEnumerable<(Request Request, Item Item)> inComing)
        {
            _mapper = mapper;
            Outgoing = new();
            Incoming = new();

            foreach (var requestInfo in outGoing)
            {
                var fOutGoing = _mapper.Map<Request, RequestViewModel>(requestInfo.Request);
                _mapper.Map(requestInfo.Item, fOutGoing);
                Outgoing.Add(fOutGoing);
            }

            foreach (var requestInfo in inComing)
            {
                var fIncoming = _mapper.Map<Request, RequestViewModel>(requestInfo.Request);
                _mapper.Map(requestInfo.Item, fIncoming);
                Incoming.Add(fIncoming);
            }
        }
    }
}
