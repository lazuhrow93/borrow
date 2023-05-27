using Borrow.Models.Identity;
using Borrow.Models.Listings;
using System;
namespace Borrow.Data.DataAccessLayer.Interfaces
{
    public interface IUserDataAccess
    {
        public List<Item> GetItems(int ownerId);
        public Item? GetItem(int ownerId, Guid id); 
        public void InsertItem(User user, Item item); 
        public void InsertItem(User user, List<Item> item);
        public bool DeleteItem(int ownerId, Guid itemIdentifer);
        
    }
}
