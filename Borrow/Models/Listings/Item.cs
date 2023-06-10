using Microsoft.EntityFrameworkCore;

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
        [Precision(18, 2)]
        public decimal DailyRate { get; set; }
        [Precision(18, 2)]
        public decimal WeeklyRate { get; set;}
        public Guid Identifier { get; set; }
        public bool IsListed { get; set; } = false;

        public Item()
        {
            Identifier = Guid.NewGuid();
            Available = true;
        }

        internal void Unlist()
        {
            this.IsListed = false;
        }

        internal void List()
        {
            this.IsListed = true;
        }
    }
}
