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
        private SignInManager<User> sm;
        private UserManager<User> um;

        public ProfileController(SignInManager<User> sm, UserManager<User> um)
        {
            sm = sm;
            um = um;
        }

        public async Task<ActionResult> Index()
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var user = await um.GetUserAsync(this.User);
            var pvm = mapper.Map<ProfileViewModel>(user);
            return View(pvm);
        }

    }
}
