using Borrow.Data.Services;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _profileService.GetCurrentUser(this.User);
            var viewModel = _profileService.GetProfileViewModel(user);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> AddItem()
        {
            return View(new AddItemViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(AddItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ItemsToSave is null) viewModel.ItemsToSave = new List<NewItemViewModel>();
                viewModel.ItemsToSave.Add(viewModel.NewItemViewModel);
            }
            return View(viewModel);
        }

        public async Task<ActionResult> SubmitItems(AddItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _profileService.GetCurrentUser(this.User);
                _profileService.CreateItems(user, viewModel);
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditItem(int ItemId)
        {
            var viewModel = _profileService.GetEditItemViewModel(ItemId);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(EditItemViewModel ivm)
        {
            _profileService.EditItem(ivm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteItems()
        {
            var user = await _profileService.GetCurrentUser(this.User);
            var profile = _profileService.GetByUser(user);
            var models = _profileService.GetDeleteItemsViewModel(profile);
            return View(models);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteItems(DeleteItemsViewModel rivm)
        {
            var user = await _profileService.GetCurrentUser(this.User);
            var selected = rivm.ItemViewModels.Where(i => i.IsSelected).Select(v=>v.Entity);
            var ids = selected.Select(i => i.ItemId);
            _profileService.DeleteItems(user, selected.Select(i=>i.ItemId));
            return RedirectToAction("Index");
        }
    }
}
