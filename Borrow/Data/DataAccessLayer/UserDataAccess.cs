using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
namespace Borrow.Data.DataAccessLayer
{
    public class UserDataAccess : IUserDataAccess
    {
        private BorrowContext _dbAccess;

        public UserDataAccess(BorrowContext bc)
        {
            _dbAccess = bc;
        }

        public List<Item> GetItems(int ownerId)
        {
            var query = _dbAccess.Item.Where(i => i.OwnerId.Equals(ownerId));
            return query.ToList();
        }

        public Item? GetItem(int ownerId, Guid itemIdentifer)
        {
            return _dbAccess.Item.Where(i => i.Identifier.Equals(itemIdentifer) && i.OwnerId.Equals(ownerId)).FirstOrDefault();
        }

        public void InsertItem(User user, Item item)
        {
            item.OwnerId = user.OwnerId;
            item.Identifier = Guid.NewGuid();
            _dbAccess.Add(item);
            _dbAccess.SaveChanges();
        }

        public void InsertItem(User user, List<Item> items)
        {
            foreach (var item in items)
            {
                item.OwnerId = user.OwnerId;
                _dbAccess.Add(item);
            }  

            _dbAccess.SaveChanges();
        }

        public bool DeleteItem(int ownerId, Guid itemIdentifer)
        {
            var toDelete = GetItem(ownerId, itemIdentifer);
            if (toDelete is null) return false;
            _dbAccess.Remove((Item)toDelete);
            _dbAccess.SaveChanges();
            return true;
        }

        public bool DeleteItem(int ownerId, List<Guid> identifier)
        {
            var toDelete = _dbAccess.Item.Where(i => identifier.Contains(i.Identifier)).ToList();
            if (toDelete is null) return false;
            _dbAccess.RemoveRange(toDelete);
            _dbAccess.SaveChanges();
            return true;
        }
    }
}
