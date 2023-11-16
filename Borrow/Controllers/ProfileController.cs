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
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        private readonly IAppProfileService _appProfileService;

        public ProfileController(
            IItemService itemService,
            IUserService userService,
            IAppProfileService appProfileServices)
        {
            _itemService = itemService;
            _userService = userService;
            _appProfileService = appProfileServices;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _userService.GetCurrentUser(this.User);
            var profile = _appProfileService.GetByUser(user);

            var items = _itemService.GetUserItems(user);
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
                var user = await _userService.GetCurrentUser(this.User);
                _itemService.CreateItems(user, viewModel);
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditItem(int ItemId)
        {
            return View(_itemService.GetItem(ItemId));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(EditItemViewModel ivm)
        {
            _itemService.EditItem(ivm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveItem()
        {
            var user = await _userService.GetCurrentUser(this.User);
            var userItems = _itemService.GetUserItems(user);
            return View(new ReviewListingsViewModel()
            {
                Items = userItems.Where(i=>!i.IsListed).ToList()
            });

        }

        [HttpPost]
        public async Task<ActionResult> RemoveItem(ReviewListingsViewModel rivm)
        {
            var user = await _userService.GetCurrentUser(this.User);
            var selected = rivm.Items.Where(i => i.IsSelected);
            var ids = selected.Select(i => i.ItemId);
            _itemService.DeleteItems(user, selected.Select(i=>i.ItemId));
            return RedirectToAction("Index");
        }
    }
}
