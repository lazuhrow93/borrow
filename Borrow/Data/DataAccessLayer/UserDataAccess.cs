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
    public class UserDataAccess
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

        public void InsertItem(AppProfile userProfile, Item item)
        {
            var user = _dbAccess.User.Where(u => u.ProfileId.Equals(userProfile.Id)).First(); //should always exist at this point?
            item.OwnerId = userProfile.OwnerId;
            item.NeighborhoodId = userProfile.NeighborhoodId;
            _dbAccess.Add(item);
            _dbAccess.SaveChanges();
        }

        public void InsertItem(AppProfile userProfile, List<Item> items)
        {
            var user = _dbAccess.User.Where(u => u.ProfileId.Equals(userProfile.Id)).First(); //should always exist at this point?
            foreach (var item in items)
            {
                item.OwnerId = userProfile.OwnerId;
                item.NeighborhoodId = userProfile.NeighborhoodId;
                _dbAccess.Add(item);
            }  

            _dbAccess.SaveChanges();
        }

        public Neighborhood? GetNeighborhood(AppProfile appProfile)
        {
            return _dbAccess.Neighborhood.Where(n => n.Id.Equals(appProfile.NeighborhoodId)).FirstOrDefault();
        }
    }
}
