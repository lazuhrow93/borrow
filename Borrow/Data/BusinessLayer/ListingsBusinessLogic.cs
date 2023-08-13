using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Identity;
using System;
using Borrow.Models.Backend;

namespace Borrow.Data.BusinessLayer
{
    public class ListingsBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
        public ListingsDataLayer ListingsDataLayer { get; set; }
        public NeighborhoodDataLayer NeighborhoodDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        public ListingsBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            RequestDataLayer = masterDL.RequestDataLayer;
            NeighborhoodDataLayer = masterDL.NeighborhoodDataLayer;
            ItemDataLayer = masterDL.ItemDataLayer;
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            ListingsDataLayer = masterDL.ListingsDataLayer;
            Mapper = mapper;
        }

        public bool Create(int itemId, decimal dailyRate, decimal weeklyRate)
        {
            var item = ItemDataLayer.Get(itemId);
            ListingsDataLayer.Insert(itemId, item.OwnerId, dailyRate, weeklyRate);
            return true;
        }

        public IEnumerable<Listing> GetNeighborhoodListings(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var neighborhood = NeighborhoodDataLayer.Get(appProfile);

            return ListingsDataLayer.GetNeighborhoodListings(neighborhood.Id);
        }

        public IEnumerable<Listing> GetUserListings(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            return ListingsDataLayer.GetOwnerListings(appProfile.OwnerId);
        }`

        public bool RemoveListing(User user, IEnumerable<int> itemIds)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            foreach (var id in itemIds)
            {
                var item = ItemDataLayer.Get(id);
                if (item.OwnerId.Equals(appProfile.OwnerId) == false) return false;
                ItemDataLayer.Delete(id);
            }
            return true;
        }
    }
}
