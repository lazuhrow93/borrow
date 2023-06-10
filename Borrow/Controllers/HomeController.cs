using AutoMapper;
using Borrow.Models;
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
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(SignInManager<User> sm, UserManager<User> um, IMapper mapper)
        {
            userManager = um;
            signInManager = sm;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hvm = new HomeViewModel(_mapper);
            if (signInManager.IsSignedIn(this.User))
            {
                var user = await userManager.GetUserAsync(this.User);
                
            }

            return View();
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