using Borrow.Models.Listings;
using System;
namespace Borrow.Data.DataAccessLayer.Interfaces
{
    public interface IUserDataAccess
    {
        public List<Item> Items(int ownerId);
        
    }
}
