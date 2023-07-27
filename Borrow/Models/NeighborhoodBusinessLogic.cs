﻿using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Identity;
using Borrow.Models.Backend;
using Borrow.Data;

namespace Borrow.Models
{
    public class NeighborhoodBusinessLogic
    {
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        public NeighborhoodDataLayer NeighborhoodDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        public NeighborhoodBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            NeighborhoodDataLayer = masterDL.NeighborhoodDataLayer;
            Mapper = mapper;
        }

        public Neighborhood Get(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            return NeighborhoodDataLayer.Get(appProfile);
        }

    }
}