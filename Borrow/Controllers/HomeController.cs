using Borrow.Models;
using Borrow.Models.Identity;
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

        public HomeController(SignInManager<User> sm, UserManager<User> um)
        {
            userManager = um;
            signInManager = sm;
        }

        public IActionResult Index()
        {
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