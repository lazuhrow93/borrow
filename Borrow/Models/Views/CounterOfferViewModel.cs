using Borrow.Models.Backend;

namespace Borrow.Models.Views
{
    public class CounterOfferViewModel
    {
        public int RequestId { get; set; }
        public string ItemName { get; set; }
        public Request.RequestType CounterRate { get; set; }
        public decimal CounterMoney { get; set; }
    }
}
