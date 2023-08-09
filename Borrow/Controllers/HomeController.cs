using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Borrow.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;

        public HomeController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia)
        {
            userManager = um;
            signInManager = sm;
            _mapper = mapper;
            _userDataAccess = ia;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hvm = new HomeViewModel();
            if (signInManager.IsSignedIn(this.User))
            {
                var user = await userManager.GetUserAsync(this.User);
                var profile = _userDataAccess.GetAppProfile(user);
                var neighborhood = _userDataAccess.GetNeighborhood(profile);
                hvm = _mapper.Map<HomeViewModel>(user);
                _mapper.Map<Neighborhood, HomeViewModel>(neighborhood, hvm);
            }
            return View(hvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}