using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;

        public AccountController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia)
        {
            userManager = um;
            signInManager = sm;
            _mapper = mapper;
            _userDataAccess = ia;
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
                var loginResult = await signInManager
                .PasswordSignInAsync(lvm.UserName, lvm.PasswordHash, isPersistent: lvm.RememberMe, lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if(!string.IsNullOrEmpty(lvm.ReturnUrl) && Url.IsLocalUrl(lvm.ReturnUrl))
                    {
                        return Redirect(lvm.ReturnUrl);
                    }
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

            var appProfile = _userDataAccess.InsertAppProfile(new Models.Backend.Neighborhood()
            {
                Id = 1
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvw)
        {
            if (ModelState.IsValid)
            {
                var newUser = _mapper.Map<User>(rvw);
                var result = await userManager.CreateAsync(newUser, rvw.Password);

                if (result.Succeeded)
                {
                    var signingIn = signInManager.SignInAsync(newUser, isPersistent: false);

                    var appProfile = _userDataAccess.InsertAppProfile(new Models.Backend.Neighborhood()
                    {
                        Id = rvw.Neighborhood
                    });

                    await signingIn;
                    var user = await userManager.GetUserAsync(this.User);
                    _userDataAccess.AssociateProfile(user, appProfile);


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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
