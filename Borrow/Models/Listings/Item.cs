namespace Borrow.Models.Listings
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OwnedSince { get; set; }
        public int OwnerId { get; set; }
        public bool Available { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set;}
    }
}
