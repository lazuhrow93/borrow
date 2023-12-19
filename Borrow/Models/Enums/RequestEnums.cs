namespace Borrow.Models.Enums
{
    public class RequestEnums
    {
        public enum Status
        {
            Pending = 10,
            Viewed = 11,
            Accepted = 12,
            ConfirmedMeetUp = 13,
            Lent = 14,
            Declined = 15,
            Expired = 16
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
