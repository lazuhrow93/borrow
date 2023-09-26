using Borrow.Models.Backend;
using Humanizer.Bytes;

namespace Borrow.Models.Views.Requests
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public int ListingId { get; set; }
        public string ItemName { get; set; }
        public string Requester { get; set; }
        public string Lender { get; set; }
        public Decimal Rate { get; set; }
        public int Periods { get; set; }
    }
}
