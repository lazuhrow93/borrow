using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;

namespace Borrow.Data.Services
{
    public class AppProfileService : IAppProfileService
    {
        private IRepository<AppProfile> _appProfileService { get; set; }

        public AppProfileService(IRepository<AppProfile> appProfileService)
        {
            _appProfileService = appProfileService;
        }

        public AppProfile? GetByUser(User user)
        {
            return _appProfileService.GetById(user.ProfileId);
        }
    }
}
