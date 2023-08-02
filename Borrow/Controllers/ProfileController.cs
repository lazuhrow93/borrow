﻿using AutoMapper;
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
        public async Task<ActionResult> EditItem(ProfileViewModel pvm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var p = _userDataAccess.GetAppProfile(user);
            var itemToEdit = _userDataAccess.GetItem(pvm.EditItem);
            var itemView = _mapper.Map<ItemViewModel>(itemToEdit);
            return View(itemView);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(ItemViewModel ivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var item = _mapper.Map<Item>(ivm);
            var p = _userDataAccess.GetAppProfile(user);
            var itemToEdit = _userDataAccess.EditItem(p, item);
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
        public async Task<ActionResult> Unlist(Guid unlistItem)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var p = _userDataAccess.GetAppProfile(user);
            var oldItem = _userDataAccess.GetItem(unlistItem);
            oldItem.Unlist();
            var newItem = oldItem;
            _userDataAccess.EditItem(p, newItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> List(Guid listItem)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var p = _userDataAccess.GetAppProfile(user);
            var oldItem = _userDataAccess.GetItem(listItem);
            oldItem.List();
            var newItem = oldItem;
            _userDataAccess.EditItem(p, newItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Remove()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var p = _userDataAccess.GetAppProfile(user);
            var rivm = new RemoveItemsViewModel();
            var userItems = _userDataAccess.GetItems(p);
            var mappedItems = _mapper.Map<List<ItemViewModel>>(userItems);
            rivm.Items = mappedItems.Select(i => { i.IsSelected = false; return i; }).ToList();
            return View(rivm);

        }

        [HttpPost]
        public async Task<ActionResult> Remove(RemoveItemsViewModel rivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var p = _userDataAccess.GetAppProfile(user);
            var itemsToDelete = _mapper.Map<List<Item>>(rivm.Items.Where(i => i.IsSelected));
            _userDataAccess.DeleteItem(p, itemsToDelete.Select(i => i.Identifier).ToList());
            return RedirectToAction("Index");

        }
    }
}
