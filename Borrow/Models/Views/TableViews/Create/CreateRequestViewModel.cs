﻿using Borrow.Models.Listings;
using static Borrow.Models.Backend.Request;

namespace Borrow.Models.Views.TableViews.Create
{
    public class CreateRequestViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public DateTime ReturnDateUtc { get; set; }
        public RequestType RequestType { get; set; }
        public decimal RequestRate { get; set; }

        public CreateRequestViewModel()
        {
            
        }

        public CreateRequestViewModel(Item item)
        {
            ItemId = item.Id;
            ItemName = item.Name;
        }
    }
}