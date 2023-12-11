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
        private readonly IProfileService _profileControllerService;

        public ProfileController(IProfileService profileControllerService)
        {
            _profileControllerService = profileControllerService;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var profile = _profileControllerService.GetByUser(user);

            var items = _profileControllerService.GetUserItems(user);
            return View(new ProfileViewModel()
            {
                Username = profile.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Exchanges = 0,
                OwnerItems = items.ToList()
            });
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
                var user = await _profileControllerService.GetCurrentUser(this.User);
                _profileControllerService.CreateItems(user, viewModel);
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditItem(int ItemId)
        {
            return View(_profileControllerService.GetItem(ItemId));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(EditItemViewModel ivm)
        {
            _profileControllerService.EditItem(ivm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteItems()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var profile = _profileControllerService.GetByUser(user);

            var models = _profileControllerService.GetDeleteItemsViewModel(profile);
            return View(models);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteItems(DeleteItemsViewModel rivm)
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var selected = rivm.ItemViewModels.Where(i => i.IsSelected).Select(v=>v.Entity);
            var ids = selected.Select(i => i.ItemId);
            _profileControllerService.DeleteItems(user, selected.Select(i=>i.ItemId));
            return RedirectToAction("Index");
        }
    }
}
