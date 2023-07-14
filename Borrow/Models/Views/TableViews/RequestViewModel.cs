﻿using Borrow.Models.Backend;
using Borrow.Models.Listings;

namespace Borrow.Models.Views.TableViews
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Request.RequestType RequestType { get; set; }
        public decimal RequestRate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string RateView { get => $"{RequestRate}/{RequestType}"; set => RateView = value; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public Request.RequestStatus Status { get; set; }

        public void Initialize(Item item)
        {
            this.ItemId = item.Id;
            this.ItemName = item.Name;
        }
    }
}
