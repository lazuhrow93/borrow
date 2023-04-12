namespace Borrow.Models.Listings
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
    }
}
