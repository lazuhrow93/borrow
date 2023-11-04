using AutoMapper;
using Borrow.Data.BusinessLayer;
using Borrow.Data.Repositories.Interfaces;
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
        private UserManager<User> _UserManager;
        private readonly IMapper _mapper;
        private readonly ListingsBusinessLogic LBL;
        private readonly ItemBusinessLogic IBL;
        private readonly AppProfileBusinessLogic ABL;

        public ProfileController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            _UserManager = um;
            _mapper = mapper;
            LBL = new(masterDL, _mapper);
            IBL = new(masterDL, _mapper);
            ABL = new(masterDL, _mapper);
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var profile = ABL.Get(user.ProfileId);

            var items = IBL.GetUserItems(user);
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
                var user = await _UserManager.GetUserAsync(this.User);
                IBL.CreateItemsForUser(user, viewModel);
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditItem(int ItemId)
        {
            return View(new EditItemViewModel(IBL.GetItem(ItemId)));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(EditItemViewModel ivm)
        {
            IBL.EditItem(ivm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveItem()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var userItems = IBL.GetUserItems(user);
            return View(new ReviewListingsViewModel()
            {
                Items = userItems.Where(i=>!i.IsListed).ToList()
            });

        }

        [HttpPost]
        public async Task<ActionResult> RemoveItem(ReviewListingsViewModel rivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var selected = rivm.Items.Where(i => i.IsSelected);
            var ids = selected.Select(i => i.ItemId);
            IBL.DeleteItem(user, selected.Select(i=>i.ItemId));
            return RedirectToAction("Index");
        }
    }
}
