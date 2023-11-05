﻿using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Repositories;
using Borrow.Models.Backend;
using Borrow.Data;
using Borrow.Models.Views.Home;

namespace Borrow.Data.BusinessLayer
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
        
        public HomeViewModel GetHomeViewModel(User user)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var neighborhood = NeighborhoodDataLayer.Get(profile);
            var homeviewmodel = Mapper.Map<HomeViewModel>(user);
            Mapper.Map<Neighborhood, HomeViewModel>(neighborhood, homeviewmodel);
            return homeviewmodel;
        }
    }
}
