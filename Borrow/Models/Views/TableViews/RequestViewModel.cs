using Borrow.Models.Backend;

namespace Borrow.Models.Views.TableViews
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Item { get; set; }
        public Request.RequestType Type { get; set; }
        public decimal Rate { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public Request.RequestStatus Status { get; set; }
    }
}
