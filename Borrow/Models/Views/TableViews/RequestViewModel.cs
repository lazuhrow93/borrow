using Borrow.Models.Backend;
using Borrow.Models.Listings;

namespace Borrow.Models.Views.TableViews
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Request.RequestType RequestType { get; set; }
        public decimal RequestRate { get; set; }
        public Request.RequestType CounterType { get; set; }
        public decimal CounterRate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string RateView { get => $"{RequestRate}/{RequestType}"; set => RateView = value; }
        public string CounterRateView { get => $"{CounterRate}/{CounterType}"; set => CounterRateView = value; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public Request.RequestStatus Status { get; set; }

        public void Initialize(Item item)
        {
            this.ItemId = item.Id;
            this.ItemName = item.Name;
        }

        public bool RequesterNeedsAction()
        {
            if (Status.Equals(Request.RequestStatus.OwnerCounter) || Status.Equals(Request.RequestStatus.Accepted))
                return true;
            return false;
        }

        public bool OwnerNeedsAction()
        {
            if (Status.Equals(Request.RequestStatus.RequesterCounter) || Status.Equals(Request.RequestStatus.Accepted))
                return true;
            return false;
        }
    }
}
