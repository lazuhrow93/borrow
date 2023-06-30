using Borrow.Models.Backend;

namespace Borrow.Models.Views.TableViews
{
    public class BorrowRequestViewModel
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public BorrowRequest.RequestStatus Status { get; set; }
    }
}
