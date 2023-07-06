﻿using Borrow.Migrations;
using Borrow.Models.Listings;
using System;

namespace Borrow.Data.DataAccessLayer
{
    public class ItemDataLayer
    {
        public BorrowContext BorrowContext { get; set; }

        public ItemDataLayer(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;
        }

        public Item? Get(Guid identifier)
        {
            return BorrowContext.Item.Where(i => i.Identifier.Equals(identifier)).FirstOrDefault();
        }

        public Item? Get(int id)
        {
            return BorrowContext.Item.Where(i=>i.Id == id).FirstOrDefault();
        }
    }
}