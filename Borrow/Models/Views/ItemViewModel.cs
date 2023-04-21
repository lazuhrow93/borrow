using Borrow.Models.Listings;

namespace Borrow.Models.Views
{
    public class ItemViewModel : Item
    {
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
        public bool Available { get; set; }
    }
}
