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
        public int NeighborhoodId { get; set; }
        public string UserName { get; set; }

        public Item()
        {
            Identifier = Guid.NewGuid();
            Available = true;
        }

        public Item(int id, string name, string description, DateTime ownedSince, bool available, 
            decimal dailyRate, decimal weeklyRate, Guid identifier, bool isListed, string userName)
        {
            Id = id;
            Name = name;
            Description = description;
            OwnedSince = ownedSince;
            Available = available;
            DailyRate = dailyRate;
            WeeklyRate = weeklyRate;
            Identifier = identifier;
            IsListed = isListed;
            UserName = userName;
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
