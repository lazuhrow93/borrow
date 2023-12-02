using AutoMapper;
using Borrow.Data.Repositories;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Home;

namespace Borrow.Data.Services.Implementations
{
    public partial class NeighborhoodService : INeighborhoodControllerService
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
    }
}
