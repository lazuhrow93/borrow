using Borrow.Models.Backend;
using Borrow.Models.Enums;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views.Requests
{
    public class CreateRequestViewModel
    {
        public ListingViewModel ListingViewModel { get; set; }
        public int PayPeriods { get; set; } = 0;
        public DateTime EstimatedReturnDateUtc { get; set; } = DateTime.UtcNow;
        public Decimal RequestRate { get; set; }
        public RequestEnums.Term TermId { get; set; }
        public Guid RequesterKey { get; set; }
        public Guid LenderKey { get; set; }

        public CreateRequestViewModel()
        {

        }
    }
}
