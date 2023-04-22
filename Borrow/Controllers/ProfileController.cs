using AutoMapper;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Setup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;

        public ProfileController(SignInManager<User> sm, UserManager<User> um)
        {
            _SignInManager = sm;
            _UserManager = um;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var u = await _UserManager.GetUserAsync(User);
            var mapper = MapperConfig.InitializeAutomapper();
            var user = await _UserManager.GetUserAsync(this.User);
            var pvm = mapper.Map<ProfileViewModel>(user);
            return View(pvm);
        }

    }
}
