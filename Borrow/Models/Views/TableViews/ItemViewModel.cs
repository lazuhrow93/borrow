using Borrow.Models.Backend;
using System;

namespace Borrow.Models.Views.TableViews
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string OwnerUserName { get; set; }
        public bool IsListed { get; set; } = false;
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnedSince { get; set; }
        public bool Available { get; set; }

        public ItemViewModel(int id, 
            string onwer,
            bool isListed,
            string itemName,
            string desc,
            string ownedSince,
            bool available)
        {
            ItemId = id;
            IsListed = IsListed;
            OwnerUserName = onwer;
            Name = itemName;
            Description = desc;
            OwnedSince = ownedSince;
            Available = available;
        }
    }
}
