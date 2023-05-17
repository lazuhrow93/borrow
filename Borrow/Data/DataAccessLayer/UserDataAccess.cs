using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Microsoft.EntityFrameworkCore;
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

        public List<Item> Items(int ownerId)
        {
            var query = _dbAccess.Item.Where(i => i.OwnerId.Equals(ownerId));
            return query.ToList();
        }

        public void InsertItem(User user, Item item)
        {
            item.OwnerId = user.OwnerId;
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
    }
}
