using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Views.TableViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    [Authorize]
    public class ListingsController : Controller
    {
        private UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly RequestBusinessLogic RBL;
        private readonly ListingsBusinessLogic LBL;
        private readonly NeighborhoodBusinessLogic NBL;

        public ListingsController(UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            _userManager = um;
            _mapper = mapper;
            _userDataAccess = ia;
            RBL = new(masterDL, _mapper);
            LBL = new(masterDL, _mapper);
            NBL = new(masterDL, _mapper);
        }

        [HttpGet]
        public async Task<ActionResult> UserListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var items = LBL.GetUserListings(user);
            return View(new UserListingsViewModel(_mapper, items));
        }

        [HttpGet]
        public async Task<ActionResult> NeighborhoodListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var items = LBL.GetNeighborhoodListings(user);
            var neighborhood = NBL.Get(user);

            return View(new NeighborhoodListingsViewModel(_mapper, items, neighborhood.Name));
        }

        [HttpPost]
        public async Task<ActionResult> ViewListing(int id)
        {
            var item = LBL.GetItemById(id);
            return View(new ViewListingViewModel(_mapper, item));
        }
    }
}
