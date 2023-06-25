using AutoMapper;
using Microsoft.Identity.Client;

namespace Borrow.Models.Backend
{
    public enum Status
    {
        Pending,
        Seen,
        Declined,
        Accepted
    }

    public class BorrowRequest
    {
        public int Id { get; set; }
        public Guid OwnerKey { get; set; }
        public Guid RequesterKey { get; set; }
        public int ItemId { get; set; }
        public Status Status { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get;set; }
    }
}
