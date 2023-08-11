using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;
using System.Net.NetworkInformation;

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
            Outgoing = new();
            Incoming = new();

            foreach (var requestInfo in outGoing)
            {
                Outgoing.Add(new RequestViewModel(requestInfo.Request, requestInfo.Item));
            }

            foreach (var requestInfo in inComing)
            {
                Incoming.Add(new RequestViewModel(requestInfo.Request, requestInfo.Item));
            }
        }
    }
}
