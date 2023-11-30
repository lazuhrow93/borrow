using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Borrow.Models.Backend
{
    public class Request : Data
    {

        public int Id { get; set; }
        public int ListingId { get; set; }
        public int ItemId { get; set; }
        public int StatusId { get; set; }
        public int PendingActionFromId { get; set; }
        public int PayPeriods { get; set; }
        [Precision(18, 2)]
        public decimal Rate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TermId { get; set; }
        public Guid LenderKey { get; set; }
        public Guid RequesterKey { get; set; }
        public DateTime? RequesterSuggestedMeetingTime { get; set; }
        public DateTime? LenderSuggestedMeetingTime { get; set; }
        public DateTime? MeetupTime { get; set; }
    }
}