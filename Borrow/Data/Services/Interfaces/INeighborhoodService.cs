using Borrow.Models.Backend;
using Borrow.Models.Views.Home;
using System;
namespace Borrow.Data.Services.Interfaces
{
    public interface INeighborhoodService
    {
        public HomeViewModel GetHomeViewModel(User user);

        public Neighborhood GetUserNeighborhood(AppProfile user);
    }
}
