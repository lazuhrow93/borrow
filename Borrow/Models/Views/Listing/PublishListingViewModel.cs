using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views.Listing
{
    public class PublishListingViewModel
    {
        public ItemViewModel ItemInfo { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }

        public PublishListingViewModel(Item item, decimal dRate = 0.0M, decimal wRate = 0.0M)
        {
            ItemInfo = new(item);
            DailyRate = dRate;
            WeeklyRate = wRate;
        }
    }
}
