using AutoMapper;
using Borrow.Data;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private readonly IMapper _mapper;
        private readonly BorrowContext _borrowContext;

        public ProfileController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, BorrowContext bc)
        {
            _SignInManager = sm;
            _UserManager = um;
            _mapper = mapper;
            _borrowContext = bc;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var u = await _UserManager.GetUserAsync(User);
            var user = await _UserManager.GetUserAsync(this.User);
            var pvm = _mapper.Map<ProfileViewModel>(user);

            var query = _borrowContext.Item.Where(i => i.OwnerId.Equals(user.OwnerId));
            List<Item> ownedItems = query.ToList();
            pvm.OwnerItems = _mapper.Map<List<ItemViewModel>>(ownedItems);

            return View(pvm);
        }

    }
}
