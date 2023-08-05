using Borrow.Models.Listings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Borrow.Models.Views.Profile
{
    public class EditItemViewModel
    {
        public int ItemId { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public decimal NewDailyRate { get; set; }
        public decimal NewWeeklyRate { get; set; }
        public DateTime OwnedSince { get; set; }

        public EditItemViewModel()
        {
            
        }

        public EditItemViewModel(Item item)
        {
            ItemId = item.Id;
            NewName = item.Name;
            NewDescription = item.Description;
            NewDailyRate = item.DailyRate;
            NewWeeklyRate = item.WeeklyRate;
            OwnedSince = item.OwnedSince;
        }
    }
}
