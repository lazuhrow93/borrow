namespace Borrow.Models.Listings
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Age { get; set; }
        public int OwnerId { get; set; }
    }
}
