using AutoMapper;
using Borrow.Data.BusinessLayer;
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
        private readonly IMasterDL _masterDl;
        private readonly NeighborhoodBusinessLogic NBL;

        public HomeController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IMasterDL masterDL)
        {
            userManager = um;
            signInManager = sm;
            _mapper = mapper;
            _masterDl = masterDL;
            NBL = new(_masterDl, _mapper);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hvm = new HomeViewModel();
            if (signInManager.IsSignedIn(this.User))
            {
                var user = await userManager.GetUserAsync(this.User);
                hvm = NBL.GetHomeViewModel(user);
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