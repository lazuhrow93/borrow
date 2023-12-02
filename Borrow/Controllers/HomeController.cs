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
        private readonly INeighborhoodControllerService _neighborhoodService;
        private readonly IProfileControllerService _profileService;

        public HomeController(
            IProfileControllerService profileControllerService,
            INeighborhoodControllerService neighborhoodService)
        {
            _profileService = profileControllerService;
            _neighborhoodService = neighborhoodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hvm = new HomeViewModel();
            if (_profileService.IsSignedIn(this.User))
            {
                var user = await _profileService.GetCurrentUser(this.User);
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