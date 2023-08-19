using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views.Listings
{
    public class PublishListingViewModel
    {
        public int NeighborhoodId { get; set; }
        public ItemViewModel ItemInfo { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
    }
}
