using AutoMapper;
using Microsoft.Identity.Client;

namespace Borrow.Models.Backend
{

    public class BorrowRequest
    {
        public enum Status
        {
            Pending,
            Seen,
            Declined,
            Accepted
        }

        public int Id { get; set; }
        public Guid OwnerKey { get; set; }
        public Guid RequesterKey { get; set; }
        public int ItemId { get; set; }
        public Status RequestStatus { get; set; } = Status.Pending;
        public DateTime ReturnDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get;set; }

        public void UpdateStatus (Status newStatus)
        {
            switch (newStatus)
            {
                case Status.Pending:
                    this.RequestStatus = newStatus;
                    break;
                case Status.Seen:
                    if (this.RequestStatus <= Status.Seen) this.RequestStatus = newStatus; //only pending can be marked to Seen...others cannot
                    break;
                case Status.Declined:
                    this.RequestStatus = newStatus;
                    break;
                case Status.Accepted:
                    this.RequestStatus = newStatus;
                    break;
                default:
                    throw new NotImplementedException($"The new Status is not implemented");
            }
        }
    }
}
