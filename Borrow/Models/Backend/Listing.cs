namespace Borrow.Models.Backend
{
    public class Listing
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
        public int OwnerId { get; set; }
    }
}
