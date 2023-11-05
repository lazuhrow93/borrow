using AutoMapper;
using Borrow.Data.Repositories;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Home;

namespace Borrow.Data.Services.Implementations
{
    public class NeighborhoodService : INeighborhoodService
    {
        private IRepository<AppProfile> _appProfileRepository;
        private IRepository<Neighborhood> _neighborhoodRepository;
        private IMapper _mapper;

        public NeighborhoodService(
            IRepository<AppProfile> appProfileRepository,
            IRepository<Neighborhood> neighborhoodRepository,
            IMapper mapper
            )
        {
            _appProfileRepository = appProfileRepository;
            _neighborhoodRepository = neighborhoodRepository;
            _mapper = mapper;
        }

        public HomeViewModel GetHomeViewModel(User user)
        {
            var profile = _appProfileRepository.GetById(user.ProfileId);
            var neighborhood = _neighborhoodRepository.GetById(profile.NeighborhoodId);
            var homeviewmodel = _mapper.Map<HomeViewModel>(user);
            _mapper.Map<Neighborhood, HomeViewModel>(neighborhood, homeviewmodel);
            return homeviewmodel;
        }
    }
}
