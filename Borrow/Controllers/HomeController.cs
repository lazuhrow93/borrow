using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models;
using Borrow.Models.Backend;
using Borrow.Models.Views.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Borrow.Controllers
{
    public class HomeController : Controller
    {
        private readonly INeighborhoodService _neighborhoodService;
        private readonly IUserService _userService;

        public HomeController(
            IUserService userService,
            INeighborhoodService neighborhoodService)
        {
            _userService = userService;
            _neighborhoodService = neighborhoodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hvm = new HomeViewModel();
            if (_userService.IsSignedIn(this.User))
            {
                var user = await _userService.GetCurrentUser(this.User);
                hvm = _neighborhoodService.GetHomeViewModel(user);
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