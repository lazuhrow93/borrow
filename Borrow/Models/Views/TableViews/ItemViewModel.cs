using Borrow.Models.Listings;
using System;

namespace Borrow.Models.Views.TableViews
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string OwnerUserName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsListed { get; set; } = false;
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnedSince { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
        public bool Available { get; set; }
        public Guid Identifier { get; set; }
    }
}
