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
        private readonly IProfileService _profileControllerService;
        private readonly IListingService _listingControllerService;

        public ListingsController(UserManager<User> um,
            IMapper mapper,
            IProfileService profileControllerService,
            IListingService controllerService)
        {
            _profileControllerService = profileControllerService;
            _listingControllerService = controllerService;
        }

        [HttpGet]
        public async Task<ActionResult> CreateListing()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var clvm = _listingControllerService.GetCreateListingViewModel(user);
            return View(clvm);
        }

        [HttpGet]
        public async Task<ActionResult> PublishListing(int itemId)
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            PublishListingViewModel p = _listingControllerService.GetPublishListingViewModel(itemId, user.ProfileId);
            return View(p);
        }

        [HttpPost]
        public async Task<ActionResult> PublishListing(PublishListingViewModel plvm)
        {
            _listingControllerService.CreateListing(plvm);
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveListings()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var listings = _listingControllerService.GetRemoveListingViewModel(user);
            return View(listings);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveListings(RemoveListingViewModel rlvm)
        {
            var listingsToDeactive = rlvm.Listings.Where(l => l.IsSelected).ToList();
            _listingControllerService.DeactivateListing(listingsToDeactive.Select(l => l.Entity.ListingId));
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> UserListings()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var listings = _listingControllerService.GetUserListingsViewModel(user);
            return View(listings);
        }

        [HttpGet]
        public async Task<ActionResult> NeighborhoodListings()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var listings = _listingControllerService.GetNeighborhoodListingsViewModel(user);

            return View(listings);
        }

        [HttpPost]
        public async Task<ActionResult> ViewListing(int listingId)
        {
            var vlvm = _listingControllerService.GetViewListingViewModel(listingId);
            return View(vlvm);
        }
    }
}
