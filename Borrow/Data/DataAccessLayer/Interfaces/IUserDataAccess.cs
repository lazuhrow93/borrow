using Borrow.Models.Identity;
using Borrow.Models.Listings;
using System;
namespace Borrow.Data.DataAccessLayer.Interfaces
{
    public interface IUserDataAccess
    {
        public AppProfile? GetAppProfile(User ownerId);
        public List<Item> GetItems(AppProfile userProfile);
        public Item? GetItem(AppProfile userProfile, Guid id);
        public void InsertItem(AppProfile userProfile, Item item); 
        public void InsertItem(AppProfile userProfile, List<Item> item);
        public bool DeleteItem(AppProfile userProfile, Guid itemIdentifer);
        public bool DeleteItem(AppProfile userProfile, List<Guid> itemIdentifers);
        public bool EditItem(AppProfile userProfile, Item newItem);
    }
}
