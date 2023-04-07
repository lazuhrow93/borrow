using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Borrow.Models.Identity;

namespace Borrow.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<User> UserManager;
        private SignInManager<User> signInManager;
        public UserController(UserManager<User> um, SignInManager<User> sm)
        {
            UserManager = um;
            signInManager = sm;
        }

        public IActionResult Login()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Regiseter()
        {

            return View();
        }

        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}
