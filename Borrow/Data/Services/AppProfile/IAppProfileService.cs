using Borrow.Models.Backend;

namespace Borrow.Data.Services
{
    public interface IAppProfileService
    {
        public AppProfile GetByUser(User user);
    }
}
