namespace Borrow.Models.Enums
{
    public class RequestEnums
    {
        public enum Status
        {
            Pending = 10,
            Viewed,
            Accepted,
            ConfirmedMeetUp,
            Lent,
            Declined,
            Expired
        }

        public enum PendingActionFrom
        {
            None = 2,
            Requester,
            Lender 
        }

        public enum Term
        {
            Daily = 1,
            Weekly = 7
        }
    }
}
