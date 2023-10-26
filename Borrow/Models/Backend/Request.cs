using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Borrow.Models.Backend
{

    public class Request : Data
    {
        public enum RequestStatus
        {
            Pending,
            Viewed,
            Accepted,
            PendingMeetUp,
            ConfirmedMeetUp,
            Lent,
            Declined,
            Expired
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
