using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
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

        public AppProfile? InsertAppProfile(Neighborhood neighborhood)
        {
            var currentOwnerId = _dbAccess.AppProfile.Max(p => p.OwnerId);
            var newAppProfile = new AppProfile()
            {
                NeighborhoodId = neighborhood.Id,
                OwnerId = currentOwnerId + 1
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
    }
}
