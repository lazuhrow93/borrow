using Borrow.Models.Backend;
using System;
namespace Borrow.Data.Repositories.Interfaces
{
    public interface IUserDataAccess
    {
        public AppProfile? InsertAppProfile(Neighborhood neighborhood);
        public bool AssociateProfile(User user, AppProfile? appProfile);
    }
}
