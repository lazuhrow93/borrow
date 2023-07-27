﻿using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Identity;
using Borrow.Models.Listings;

namespace Borrow.Models
{
    public class ListingsBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
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
            Mapper = mapper;
        }

        public Item GetItemById(int id)
        {
            var item = ItemDataLayer.Get(id);
            if (item is not null) return item;
            else throw new Exception($"Unable to find the item with id {id}");
        }

        public IEnumerable<Item> GetNeighborhoodListings(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var neighborhood = NeighborhoodDataLayer.Get(appProfile);
            var items = ItemDataLayer.Get(neighborhood);

            return items.Where(i => i.OwnerId.Equals(appProfile.OwnerId) == false);
        }

        public IEnumerable<Item> GetUserListings(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            return ItemDataLayer.GetOwnerItems(appProfile.OwnerId);
        }
    }
}