namespace Borrow.Models.Enums
{
    public class RequestEnums
    {
        public enum Status
        {
            Pending,
            Viewed,
            Accepted,
            ConfirmedMeetUp,
            Lent,
            Declined,
            Expired
        }

        public enum PendingActionFrom
        {
            None,
            Requester,
            Lender
        }

        public enum Term
        {
            Daily,
            Weekly
        }
    }
}
