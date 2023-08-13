using AutoMapper;
using Borrow.Data.BusinessLayer;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Views.Listing;
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
        private readonly RequestBusinessLogic RBL;
        private readonly ListingsBusinessLogic LBL;
        private readonly NeighborhoodBusinessLogic NBL;
        private readonly ItemBusinessLogic IBL;

        public ListingsController(UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            _userManager = um;
            _mapper = mapper;
            IBL = new(masterDL, _mapper);
            RBL = new(masterDL, _mapper);
            LBL = new(masterDL, _mapper);
            NBL = new(masterDL, _mapper);
        }

        [HttpGet]
        public async Task<ActionResult> CreateListing()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var items = IBL.GetAvailableItems(user);
            var clvm = new CreateListingViewModel(items);
            return View(clvm);
        }

        [HttpGet]
        public async Task<ActionResult> PublishListing(int itemId)
        {
            var item = IBL.GetItem(itemId);
            var plvm = new PublishListingViewModel(item, 0.0M, 0.0M);
            return View(plvm);
        }

        [HttpPost]
        public async Task<ActionResult> PublishListing(PublishListingViewModel plvm)
        {
            LBL.Create(plvm.ItemInfo.ItemId, plvm.DailyRate, plvm.WeeklyRate);
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> UserListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var listings = LBL.GetUserListings(user);
            return View(new UserListingsViewModel(listings));
        }

        [HttpGet]
        public async Task<ActionResult> NeighborhoodListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var listings = LBL.GetNeighborhoodListings(user);
            var neighborhood = NBL.Get(user);

            return View(new NeighborhoodListingsViewModel(listings, neighborhood.Name));
        }

        [HttpPost]
        public async Task<ActionResult> ViewListing(int id)
        {
            var item = IBL.GetItem(id);
            return View(new ViewListingViewModel(_mapper, item));
        }
    }
}
