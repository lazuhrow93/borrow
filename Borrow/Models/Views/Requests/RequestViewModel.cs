using Borrow.Migrations;
using Borrow.Models.Enums;
using static Borrow.Models.Backend.Request;

namespace Borrow.Models.Views.Requests
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public int ListingId { get; set; }
        public string ItemName { get; set; }
        public string Requester { get; set; }
        public string Lender { get; set; }
        public int Term { get; set; }
        public Decimal Rate { get; set; }
        public int Periods { get; set; }
        public int StatusId { get; set; }
        public int PendingActionFromId { get; set; }
        public DateTime MeetupTime { get; set; }

        public (string Lender, string Requester) GetStatus()
        {
            switch((RequestEnums.Status)StatusId)
            {
                case RequestEnums.Status.Pending:
                    return ("New!", "Pending");
                case RequestEnums.Status.Expired:
                    return ("Expired", "Expired");
                case RequestEnums.Status.Declined:
                    return ("You Declined", "The Owner Declined");
                case RequestEnums.Status.Lent:
                    return ("Lent", "You currently have this item");
                case RequestEnums.Status.ConfirmedMeetUp:
                    return ("Time to meetup", "Time to meetup");
                case RequestEnums.Status.Accepted:
                    return ("Pending Requester's meet up time", "Enter time To meetup");
               case RequestEnums.Status.Viewed:
                    return ("Decline or Accept", "Owner Viewed");
               default:
                    return ("Something is wrong", "Something is wrong");
            }
        }

    }
}
