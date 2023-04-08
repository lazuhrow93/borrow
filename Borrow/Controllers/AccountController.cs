using Borrow.Models;
using Borrow.Models.Identity.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(SignInManager<User> sm, UserManager<User> um)
        {
            userManager = um;
            signInManager = sm;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = lvm.UserName };
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvw)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    FirstName = rvw.FirstName,
                    LastName = rvw.LastName,
                    UserName = rvw.Username,
                    Email = rvw.Email,
                    PhoneNumber = rvw.PhoneNumber
                };
                var result = await userManager.CreateAsync(newUser, rvw.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(newUser, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(rvw);
        }

        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}
