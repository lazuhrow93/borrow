using Microsoft.EntityFrameworkCore;

namespace Borrow.Models.Backend
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OwnedSince { get; set; }
        public int OwnerId { get; set; }
        public bool Available { get; set; }
        public bool IsListed { get; set; } = false;
        public int NeighborhoodId { get; set; }

        public Item()
        {
            Available = true;
        }
    }
}
