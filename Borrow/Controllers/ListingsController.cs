using AutoMapper;
using Borrow.Data.BusinessLayer;
using Borrow.Data.Repositories;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Listings;
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
        private readonly ListingsBusinessLogic LBL;

        public ListingsController(UserManager<User> um, IMapper mapper,IMasterDL masterDL)
        {
            _userManager = um;
            _mapper = mapper;
            LBL = new(masterDL, _mapper);
        }

        [HttpGet]
        public async Task<ActionResult> CreateListing()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var clvm = LBL.GetCreateListingViewModel(user);
            return View(clvm);
        }

        [HttpGet]
        public async Task<ActionResult> PublishListing(int itemId)
        {
            var user = await _userManager.GetUserAsync(this.User);
            PublishListingViewModel p = LBL.GetPublishListingViewModel(itemId, user.ProfileId);
            return View(p);
        }

        [HttpPost]
        public async Task<ActionResult> PublishListing(PublishListingViewModel plvm)
        {
            LBL.CreateListing(plvm);
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveListing()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var listings = LBL.GetRemoveListingViewModel(user);
            return View(listings);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveListing(RemoveListingViewModel rlvm)
        {
            var user = await _userManager.GetUserAsync(this.User);
            var listingsToDeactive = rlvm.Listings.Where(l => l.IsSelected).ToList();
            var listings = LBL.DeactiveListing(listingsToDeactive.Select(l => l.Entity.ListingId));
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> UserListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var listings = LBL.GetUserListingsViewModel(user);
            return View(listings);
        }

        [HttpGet]
        public async Task<ActionResult> NeighborhoodListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var listings = LBL.GetNeighborhoodListingsViewModel(user);

            return View(listings);
        }

        [HttpPost]
        public async Task<ActionResult> ViewListing(int listingId)
        {
            var vlvm = LBL.GetViewListingViewModel(listingId);
            return View(vlvm);
        }
    }
}
