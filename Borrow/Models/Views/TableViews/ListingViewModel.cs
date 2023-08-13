using Borrow.Models.Backend;
using Microsoft.AspNetCore.Http.Features;

namespace Borrow.Models.Views.TableViews
{
    public class ListingViewModel
    {
        public int ListingId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
        public string OwnerUsername { get; set; }
        public int OwnerId { get; set; }

        public ListingViewModel()
        {
                
        }

        public ListingViewModel(int id, int itemId, string item, string description, decimal dr, decimal wr, string owner, int ownerid)
        {
            ListingId = id;
            ItemId = itemId;
            ItemName = item;
            Description = description;
            DailyRate = dr;
            WeeklyRate = wr;
            OwnerUsername = owner;
            OwnerId = ownerid;
        }
    }
}
