using static Borrow.Models.Backend.Request;

namespace Borrow.Models.Views.Requests
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public int ListingId { get; set; }
        public string ItemName { get; set; }
        public string Requester { get; set; }
        public string Lender { get; set; }
        public int Term { get; set; }
        public Decimal Rate { get; set; }
        public int Periods { get; set; }
        public int StatusId { get; set; }
        public int PendingActionFromId { get; set; }
        public DateTime MeetupTime { get; set; }
    }
}
