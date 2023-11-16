using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Views;
using Borrow.Models.Backend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Borrow.Data.Services.Interfaces;

namespace Borrow.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string ReturnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = ReturnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            
            if (ModelState.IsValid)
            {
                if ((await _userService.Login(lvm)).Succeeded)
                {
                    if(!string.IsNullOrEmpty(lvm.ReturnUrl) && Url.IsLocalUrl(lvm.ReturnUrl))
                        return Redirect(lvm.ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username/password");
            return View(lvm);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var successRegister = await _userService.Register(rvm);

                if (successRegister)
                    return RedirectToAction("Index", "Home");
            }
            return View(rvm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
