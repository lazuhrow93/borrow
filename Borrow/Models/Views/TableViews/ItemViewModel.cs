using Borrow.Models.Backend;
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

        public ItemViewModel()
        {
            
        }

        public ItemViewModel(Item item)
        {
            ItemId = item.Id;
            Name = item.Name;
            Description = item.Description;
            OwnedSince = item.OwnedSince.ToString("MM/dd/yyyy");
            Available = item.Available;
        }
    }
}
