using Borrow.Models.Identity;
using Borrow.Models.Listings;
using System;
namespace Borrow.Data.DataAccessLayer.Interfaces
{
    public interface IUserDataAccess
    {
        public List<Item> Items(int ownerId);
        public void InsertItem(User user, Item item); 
        public void InsertItem(User user, List<Item> item); 

    }
}
