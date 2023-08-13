using Borrow.Models.Backend;

namespace Borrow.Models.Views.TableViews
{
    public class ListingViewModel
    {
        public int ItemId { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
        public string OwnerUsername { get; set; }
        public int OwnerId { get; set; }

        public ListingViewModel()
        {
                
        }

        public ListingViewModel(Backend.Listing rawlisting)
        {
            ItemId = rawlisting.ItemId;
            DailyRate = rawlisting.DailyRate;
            WeeklyRate= rawlisting.WeeklyRate;
            OwnerUsername = rawlisting.OwnerUserName;
        }
    }
}
