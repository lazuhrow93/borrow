using AutoMapper;
using AutoMapper.Internal;
using Borrow.Models.Backend;

namespace Borrow.Models.Views.TableViews
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Request.RequestType RequestType { get; set; }
        public decimal RequestRate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string RateView { get => $"{RequestRate}/{RequestType}"; set => RateView = value; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public Request.RequestStatus Status { get; set; }

        public RequestViewModel() { }

        public RequestViewModel(Request request, Item item)
        {
            RequestId = request.Id;
            ItemId = request.ItemId;
            ItemName = item.Name;
            RequestType = (Request.RequestType)request.Type;
            RequestRate = request.Rate;
            ReturnDate = request.ReturnDate;
            CreatedDateUtc = request.CreatedDateUtc;
            Status = request.Status;
        }

        public bool RequesterNeedsAction()
        {
            if (Status.Equals(Request.RequestStatus.Accepted))
                return true;
            return false;
        }

        public bool OwnerNeedsAction()
        {
            if (Status.Equals(Request.RequestStatus.Pending) || Status.Equals(Request.RequestStatus.Viewed) || 
                Status.Equals(Request.RequestStatus.Accepted))
                return true;
            return false;
        }
    }
}
