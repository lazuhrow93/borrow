using AutoMapper;
using Borrow.Data.BusinessLayer;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
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
            var listings = LBL.GetUserListings(user);
            return View(new RemoveListingViewModel()
            {
                Listings = listings.Select(l =>
                {
                    return new SelectorViewModel<ListingViewModel>()
                    {
                        IsSelected = false,
                        Entity = l
                    };
                }).ToList()
            });
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
            var listings = LBL.GetUserListings(user);
            return View(new UserListingsViewModel(listings));
        }

        [HttpGet]
        public async Task<ActionResult> NeighborhoodListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var neighborhood = NBL.Get(user);
            var listings = LBL.GetNeighborhoodListings(neighborhood);

            return View(new NeighborhoodListingsViewModel(listings, neighborhood.Name));
        }

        [HttpPost]
        public async Task<ActionResult> ViewListing(int id)
        {
            var listing = LBL.GetListing(id);
            return View(new ViewListingViewModel(listing));
        }
    }
}
