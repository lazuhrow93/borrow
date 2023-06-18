using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Views;
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

        public ListingsController(UserManager<User> um, IMapper mapper, IUserDataAccess ia)
        {
            _userManager = um;
            _mapper = mapper;
            _userDataAccess = ia;
        }

        [HttpGet]
        public async Task<ActionResult> UserListings()
        {
            var ulvm = new UserListingsViewModel(_mapper);
            var user = await _userManager.GetUserAsync(this.User);
            var profile = _userDataAccess.GetAppProfile(user);
            ulvm.MapItems(_userDataAccess.GetItems(profile));
            return View(ulvm);
        }

        [HttpGet]
        public async Task<ActionResult> NeighborhoodListings()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var profile = _userDataAccess.GetAppProfile(user);
            var neighborhood = _userDataAccess.GetNeighborhood(profile);
            var neighborhoodItems = _userDataAccess.GetNeighborhoodItems(profile);
            var nlvm = new NeighborhoodListingsViewModel(_mapper);
            nlvm.Name = neighborhood.Name;
            nlvm.OrganizeItems(neighborhoodItems);

            return View(nlvm);
        }

        [HttpPost]
        public async Task<ActionResult> ViewListing(Guid Identifier)
        {
            var user = await _userManager.GetUserAsync(this.User);
            var profile = _userDataAccess.GetAppProfile(user);
            var item = _userDataAccess.GetItem(profile, Identifier);
            var vlvm = new ViewListingViewModel();
            vlvm.ItemViewModel = _mapper.Map<ItemViewModel>(item);
            return View(vlvm);
        }

        // POST: ListingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListingsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
