﻿using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Borrow.Models.Views.Profile
{
    public class EditItemViewModel
    {
        public int ItemId { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public DateTime OwnedSince { get; set; }

        public EditItemViewModel()
        {
            
        }

        public EditItemViewModel(ItemViewModel currentItemInfo)
        {
            ItemId = currentItemInfo.ItemId;
            NewName = currentItemInfo.Name;
            NewDescription = currentItemInfo.Description;
            OwnedSince = DateTime.Parse(currentItemInfo.OwnedSince);
        }
    }
}
