using Borrow.Models.Backend;

namespace Borrow.Models.Views.TableViews
{
    public class BorrowRequestViewModel
    {
        public string Item { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public Status Status { get; set; }
    }
}
