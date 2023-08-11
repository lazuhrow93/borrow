using Borrow.Models.Backend;
using Borrow.Models.Identity;
using System;
namespace Borrow.Data.DataAccessLayer.Interfaces
{
    public interface IUserDataAccess
    {
        public AppProfile? GetAppProfile(User ownerId);
        public AppProfile? InsertAppProfile(Neighborhood neighborhood);
        public bool AssociateProfile(User user, AppProfile? appProfile);
        public Neighborhood? GetNeighborhood(AppProfile appProfile);
        public List<Item> GetNeighborhoodItems(AppProfile userProfile);
        public List<Item> GetItems(AppProfile neighborhood);
        public void InsertItem(AppProfile userProfile, Item item); 
        public void InsertItem(AppProfile userProfile, List<Item> item);
        public bool DeleteItem(AppProfile userProfile, Guid itemIdentifer);
        public bool DeleteItem(AppProfile userProfile, List<Guid> itemIdentifers);
    }
}
