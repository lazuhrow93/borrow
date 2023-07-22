using Borrow.Migrations;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
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

        public IEnumerable<Item> GetOwnerItems(int ownerId)
        {
            return BorrowContext.Item.Where(i => i.OwnerId.Equals(ownerId));
        }

        public IEnumerable<Item>? Get(Neighborhood neighborhood)
        {
            return BorrowContext.Item.Where(i => i.NeighborhoodId.Equals(neighborhood.Id));
        }

        public bool Update(Item newItem)
        {
            var currentItem = BorrowContext.Item.Where(i=>i.Id == newItem.Id).FirstOrDefault();
            if (currentItem == null) return false;
            currentItem = newItem;
            BorrowContext.SaveChanges();
            return true;
        }
    }
}