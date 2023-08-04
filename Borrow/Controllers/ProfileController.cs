using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
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
            return View(new ItemViewModel(item));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(ItemViewModel ivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var newItem = new Item(ivm.ItemId, ivm.Name, ivm.Description, DateTime.Parse(ivm.OwnedSince),
                ivm.Available, ivm.DailyRate, ivm.WeeklyRate, ivm.Identifier, ivm.IsListed, ivm.OwnerUserName);
            var itemToEdit = LBL.EditItem(user, newItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> ListUnlist()
        {
            var elvm = new EditListingsViewModel(_mapper);
            var user = await _UserManager.GetUserAsync(this.User);
            var p = _userDataAccess.GetAppProfile(user);
            elvm.MapItems(_userDataAccess.GetItems(p));
            return View(elvm);
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
            var ivms = userItems.Select(i=> { return new ItemViewModel(i); });
            var rivm = new RemoveItemsViewModel();
            rivm.Items = ivms.Select(i => { i.IsSelected = false; return i; }).ToList();
            return View(rivm);

        }

        [HttpPost]
        public async Task<ActionResult> Remove(RemoveItemsViewModel rivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            LBL.RemoveListing(user, rivm.Items.Where(i => i.IsSelected).Select(i=>i.ItemId));
            return RedirectToAction("Index");

        }
    }
}
