﻿namespace Borrow.Models.Backend
{

    public class Request : Data
    {
        public enum RequestStatus
        {
            Pending,
            Viewed,
            Accepted,
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
        public Guid LenderKey { get; set; }
        public Guid RequesterKey { get; set; }
        public int ItemId { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public int PayPeriods { get; set; }

        public DateTime ReturnDate { get; set; }
        public RequestType? Type { get; set; } = RequestType.Daily;

        public void UpdateStatus (RequestStatus newStatus)
        {
            switch (newStatus)
            {
                case RequestStatus.Pending:
                    this.Status = newStatus;
                    break;
                case RequestStatus.Viewed:
                    if (this.Status <= RequestStatus.Viewed) this.Status = newStatus; //only pending can be marked to Seen...others cannot
                    break;
                case RequestStatus.Declined:
                    this.Status = newStatus;
                    break;
                case RequestStatus.Accepted:
                    this.Status = newStatus;
                    break;
                default:
                    throw new NotImplementedException($"The new Status is not implemented");
            }
        }
    }
}
