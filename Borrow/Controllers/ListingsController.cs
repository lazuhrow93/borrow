using AutoMapper;
using Borrow.Data.Services;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Listings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    [Authorize]
    public class ListingsController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IUserService _userService;

        public ListingsController(UserManager<User> um,
            IMapper mapper,
            IUserService userService,
            IListingService listingService)
        {
            _userService = userService;
            _listingService = listingService;
        }

        [HttpGet]
        public async Task<ActionResult> CreateListing()
        {
            var user = await _userService.GetCurrentUser(this.User);
            var clvm = _listingService.GetCreateListingViewModel(user);
            return View(clvm);
        }

        [HttpGet]
        public async Task<ActionResult> PublishListing(int itemId)
        {
            var user = await _userService.GetCurrentUser(this.User);
            PublishListingViewModel p = _listingService.GetPublishListingViewModel(itemId, user.ProfileId);
            return View(p);
        }

        [HttpPost]
        public async Task<ActionResult> PublishListing(PublishListingViewModel plvm)
        {
            _listingService.CreateListing(plvm);
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveListing()
        {
            var user = await _userService.GetCurrentUser(this.User);
            var listings = _listingService.GetRemoveListingViewModel(user);
            return View(listings);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveListing(RemoveListingViewModel rlvm)
        {
            var user = await _userService.GetCurrentUser(this.User);
            var listingsToDeactive = rlvm.Listings.Where(l => l.IsSelected).ToList();
            var listings = _listingService.DeactiveListing(listingsToDeactive.Select(l => l.Entity.ListingId));
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> UserListings()
        {
            var user = await _userService.GetCurrentUser(this.User);
            var listings = _listingService.GetUserListingsViewModel(user);
            return View(listings);
        }

        [HttpGet]
        public async Task<ActionResult> NeighborhoodListings()
        {
            var user = await _userService.GetCurrentUser(this.User);
            var listings = _listingService.GetNeighborhoodListingsViewModel(user);

            return View(listings);
        }

        [HttpPost]
        public async Task<ActionResult> ViewListing(int listingId)
        {
            var vlvm = _listingService.GetViewListingViewModel(listingId);
            return View(vlvm);
        }
    }
}
