﻿using Borrow.Models.Listings;
using System;

namespace Borrow.Models.Views.Item
{
    public class ItemViewModel
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnedSince { get; set; }
        public decimal DailyRate { get; set; }
        public decimal WeeklyRate { get; set; }
        public bool Available { get; set; }
        public Guid Identifier { get; set; }
    }
}
