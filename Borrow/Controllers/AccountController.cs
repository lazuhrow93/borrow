using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Views;
using Borrow.Models.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Borrow.Data.BusinessLayer;

namespace Borrow.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IMasterDL _masterDL;

        public AccountController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            userManager = um;
            signInManager = sm;
            _mapper = mapper;
            _userDataAccess = ia;
            _masterDL  = masterDL;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvw)
        {
            if (ModelState.IsValid)
            {
                var neighborhood = new Neighborhood()
                {
                    Id = rvw.Neighborhood
                };

                if(_masterDL.NeighborhoodDataLayer.Exist(neighborhood) == false)
                    return View("NoNeighborhood");

                var newUser = _mapper.Map<User>(rvw);
                var result = await userManager.CreateAsync(newUser, rvw.Password);

                if (result.Succeeded)
                {
                    var signingIn = signInManager.SignInAsync(newUser, isPersistent: false);
                    var currentNeighborhood = _masterDL.NeighborhoodDataLayer.Get(neighborhood);
                    var appProfile = _userDataAccess.InsertAppProfile(currentNeighborhood);
                    await signingIn;
                    
                    var user = await userManager.GetUserAsync(this.User);
                    _userDataAccess.AssociateProfile(newUser, appProfile);


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
