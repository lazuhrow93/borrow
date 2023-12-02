using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Home;

namespace Borrow.Data.Services.Implementations
{
    public partial class NeighborhoodService : INeighborhoodControllerService
    {
        public HomeViewModel GetHomeViewModel(User user)
        {
            var profile = _appProfileRepository.GetById(user.ProfileId);
            var neighborhood = _neighborhoodRepository.GetById(profile.NeighborhoodId);
            var homeviewmodel = _mapper.Map<HomeViewModel>(user);
            _mapper.Map<Neighborhood, HomeViewModel>(neighborhood, homeviewmodel);
            return homeviewmodel;
        }

        public Neighborhood GetUserNeighborhood(AppProfile profile)
        {
            return _neighborhoodRepository.GetById(profile.NeighborhoodId);
        }
    }
}
