﻿using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;

namespace Borrow.Models.Views.Listings
{
    public class CreateListingViewModel
    {
        public List<ItemViewModel> AvailableItems { get; set; }

        public CreateListingViewModel() { }
    }
}
