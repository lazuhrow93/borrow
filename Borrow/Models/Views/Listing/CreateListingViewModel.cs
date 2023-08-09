using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views.Listing
{
    public class CreateListingViewModel
    {
        public ItemViewModel ItemInfo { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }

        public CreateListingViewModel() { }

        public CreateListingViewModel(Item item, decimal dailyRate, decimal weeklyRate)
        {
            ItemInfo = new(item);
            DailyRate = dailyRate;
            WeeklyRate = weeklyRate;
        }
    }
}
