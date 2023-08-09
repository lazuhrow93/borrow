using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Borrow.Models.Views.TableViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IMasterDL _masterDL;
        private readonly ListingsBusinessLogic LBL;

        public ProfileController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            _SignInManager = sm;
            _UserManager = um;
            _mapper = mapper;
            _userDataAccess = ia;
            LBL = new(masterDL, _mapper);
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var items = LBL.GetUserListings(user);
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
                LBL.InsertItem(user, viewModel.ItemsToSave.Select(ni => ni.Parse()).ToList());
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditItem(int ItemId)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var item = LBL.GetItemById(ItemId);
            return View(new EditItemViewModel(item));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(EditItemViewModel ivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            LBL.EditItem(user, ivm.ItemId, ivm.NewName, ivm.NewDescription, ivm.NewDailyRate, ivm.NewWeeklyRate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> ListUnlist()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            return View(new EditListingsViewModel(LBL.GetUserListings(user)));
        }

        [HttpPost]
        public async Task<ActionResult> Unlist(int ItemId)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            LBL.ChangeListingStatus(user, ItemId, false);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> List(int ItemId)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            LBL.ChangeListingStatus(user, ItemId, true);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Remove()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var userItems = LBL.GetUserListings(user);
            return View(new RemoveItemsViewModel(userItems));

        }

        [HttpPost]
        public async Task<ActionResult> Remove(RemoveItemsViewModel rivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var selected = rivm.Items.Where(i => i.IsSelected);
            var ids = selected.Select(i => i.ItemId);
            LBL.RemoveListing(user, selected.Select(i=>i.ItemId));
            return RedirectToAction("Index");

        }
    }
}
