using Microsoft.EntityFrameworkCore;

namespace Borrow.Models.Backend
{
    public class Listing
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        [Precision(18, 2)]
        public decimal DailyRate { get; set; }
        [Precision(18, 2)]
        public decimal WeeklyRate { get; set; }
        public int OwnerId { get; set; }
        public int NeighborhoodId { get; set; }
        public bool Active { get; set; }
        public bool Dealt { get; set; }
    }
}
