using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
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

        public AppProfile? InsertAppProfile(Neighborhood neighborhood)
        {
            var currentOwnerId = _dbAccess.AppProfile.Max(p => p.OwnerId);
            var newAppProfile = new AppProfile()
            {
                NeighborhoodId = neighborhood.Id,
                OwnerId = currentOwnerId+1
            };

            var t = _dbAccess.Add(newAppProfile);
            var y = t.Entity;
            _dbAccess.SaveChanges();
            return t.Entity;
        }

        public bool AssociateProfile(User user, AppProfile profile)
        {
            var currentUser = _dbAccess.User.Where(u => u.Id.Equals(user.Id)).FirstOrDefault();
            currentUser.ProfileId = profile.Id;
            _dbAccess.SaveChanges();
            return true;
        }

        public List<Item> GetNeighborhoodItems(AppProfile userProfile)
        {
            return _dbAccess.Item.Where(i=>i.NeighborhoodId.Equals(userProfile.NeighborhoodId)).ToList();
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
            var user = _dbAccess.User.Where(u => u.ProfileId.Equals(userProfile.Id)).First(); //should always exist at this point?
            item.UserName = user.UserName;
            item.OwnerId = userProfile.OwnerId;
            item.NeighborhoodId = userProfile.NeighborhoodId;
            item.Identifier = Guid.NewGuid();
            _dbAccess.Add(item);
            _dbAccess.SaveChanges();
        }

        public void InsertItem(AppProfile userProfile, List<Item> items)
        {
            var user = _dbAccess.User.Where(u => u.ProfileId.Equals(userProfile.Id)).First(); //should always exist at this point?
            foreach (var item in items)
            {
                item.UserName = user.UserName;
                item.OwnerId = userProfile.OwnerId;
                item.NeighborhoodId = userProfile.NeighborhoodId;
                item.Identifier = Guid.NewGuid();
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
            var currentItem = GetItem(userProfile, newItem.Identifier);
            if (currentItem is null) return false;
            currentItem.WeeklyRate = newItem.WeeklyRate;
            currentItem.DailyRate = newItem.DailyRate;
            currentItem.Available = newItem.Available;
            currentItem.Description = newItem.Description;
            currentItem.Name = newItem.Name;
            _dbAccess.SaveChanges();
            return true;
        }

        public Neighborhood? GetNeighborhood(AppProfile appProfile)
        {
            return _dbAccess.Neighborhood.Where(n => n.Id.Equals(appProfile.NeighborhoodId)).FirstOrDefault();
        }
    }
}
