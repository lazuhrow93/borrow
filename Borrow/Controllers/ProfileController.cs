using AutoMapper;
using Borrow.Data.BusinessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private readonly IMapper _mapper;
        private readonly IMasterDL _masterDL;
        private readonly ListingsBusinessLogic LBL;
        private readonly ItemBusinessLogic IBL;

        public ProfileController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            _SignInManager = sm;
            _UserManager = um;
            _mapper = mapper;
            LBL = new(masterDL, _mapper);
            IBL = new(masterDL, _mapper);
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var items = IBL.GetUserItems(user);
            return View(new ProfileViewModel(user, items));
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
                IBL.CreateItemForUser(user, viewModel.ItemsToSave.Select(ni => ni.Parse()).ToList());
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditItem(int ItemId)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var item = IBL.GetItem(ItemId);
            return View(new EditItemViewModel(item));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(EditItemViewModel ivm)
        {
            IBL.EditItem(ivm.ItemId, ivm.NewName, ivm.NewDescription, ivm.NewDailyRate, ivm.NewWeeklyRate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Remove()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var userItems = IBL.GetUserItems(user);
            return View(new ReviewListingsViewModel(userItems));

        }

        [HttpPost]
        public async Task<ActionResult> Remove(ReviewListingsViewModel rivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var selected = rivm.Items.Where(i => i.IsSelected);
            var ids = selected.Select(i => i.ItemId);
            LBL.RemoveListing(user, selected.Select(i=>i.ItemId));
            return RedirectToAction("Index");

        }
    }
}
