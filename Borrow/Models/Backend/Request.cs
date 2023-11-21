using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Borrow.Models.Backend
{

    public class Request : Data
    {
        public enum RequestStatus
        {
            Pending = (1 << 0),
            Viewed = (1 << 1),
            Accepted = (1 << 2),
            ConfirmedMeetUp = (1 << 4),
            Lent = (1 << 5),
            Declined = (1 << 6),
            Expired = (1 << 7)
        }

        public enum WaitingOn
        {
            None,
            Requester,
            Lender
        }

        public enum RequestType
        {
            Daily,
            Weekly
        }

        public int Id { get; set; }
        public int ListingId { get; set; }
        public int ItemId { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public WaitingOn ActionNeededFrom { get; set; } = WaitingOn.None;
        public int PayPeriods { get; set; }
        [Precision(18, 2)]
        public decimal Rate { get; set; }
        public DateTime ReturnDate { get; set; }
        public RequestType? Type { get; set; } = RequestType.Daily;
        public Guid LenderKey { get; set; }
        public Guid RequesterKey { get; set; }
        public DateTime? SuggestedMeetingTime { get; set; }
        public DateTime? MeetupTime { get; set; }
    }
}
