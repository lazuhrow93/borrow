using AutoMapper;
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

        public AppProfile? GetAppProfile(User user)
        {
            return _dbAccess.AppProfile.Where(p => p.Id.Equals(user.ProfileId)).FirstOrDefault();
        }

        public List<Item> GetItems(AppProfile userProfile)
        {
            var query = _dbAccess.Item.Where(i => i.OwnerId.Equals(userProfile.OwnerId));
            return query.ToList();
        }

        public Item? GetItem(AppProfile userProfile, Guid itemIdentifer)
        {
            return _dbAccess.Item.SingleOrDefault(i => i.Identifier.Equals(itemIdentifer));
        }

        public void InsertItem(AppProfile userProfile, Item item)
        {
            item.OwnerId = userProfile.OwnerId;
            item.Identifier = Guid.NewGuid();
            _dbAccess.Add(item);
            _dbAccess.SaveChanges();
        }

        public void InsertItem(AppProfile userProfile, List<Item> items)
        {
            foreach (var item in items)
            {
                item.OwnerId = userProfile.OwnerId;
                _dbAccess.Add(item);
            }  

            _dbAccess.SaveChanges();
        }

        public bool DeleteItem(AppProfile userProfile, Guid itemIdentifer)
        {
            var toDelete = GetItem(userProfile, itemIdentifer);
            if (toDelete is null) return false;
            _dbAccess.Remove((Item)toDelete);
            _dbAccess.SaveChanges();
            return true;
        }

        public bool DeleteItem(AppProfile userProfile, List<Guid> identifier)
        {
            var toDelete = _dbAccess.Item.Where(i => identifier.Contains(i.Identifier)).ToList();
            if (toDelete is null) return false;
            _dbAccess.RemoveRange(toDelete);
            _dbAccess.SaveChanges();
            return true;
        }

        public bool EditItem(AppProfile userProfile, Item newItem) {
            var currentItem = GetItem(userProfile, newItem.Identifier);//_dbAccess.Item.SingleOrDefault(i => i.Identifier.Equals(newItem.Identifier));
            if (currentItem is null) return false;
            currentItem.WeeklyRate = newItem.WeeklyRate;
            currentItem.DailyRate = newItem.DailyRate;
            currentItem.Available = newItem.Available;
            currentItem.Description = newItem.Description;
            currentItem.Name = newItem.Name;
            _dbAccess.SaveChanges();
            return true;
        }
    }
}
