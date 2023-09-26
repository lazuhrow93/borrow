using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;
using static Borrow.Models.Backend.Request;

namespace Borrow.Models.Views.Requests
{
    public class CreateRequestViewModel
    {
        public ListingViewModel ListingViewModel { get; set; }
        public int PayPeriods { get; set; } = 0;
        public DateTime EstimatedReturnDateUtc { get; set; } = DateTime.UtcNow;
        public Decimal RequestRate { get; set; }
        public RequestType RequestType { get; set; } = RequestType.Daily;
        public Guid RequesterKey { get; set; }
        public Guid LenderKey { get; set; }

        public CreateRequestViewModel()
        {

        }
    }
}
