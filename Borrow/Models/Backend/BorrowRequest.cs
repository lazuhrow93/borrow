using Microsoft.Identity.Client;

namespace Borrow.Models.Backend
{
    public class BorrowRequest
    {
        public int Id { get; set; }
        public int RequestDetailsId { get; set; }
        public int OwnerProfileId { get; set; }
        public int RequesterProfileId { get; set; }
        public int ItemId { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get;set; }
    }
}
