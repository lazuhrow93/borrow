﻿using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
using Borrow.Models.Views.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;

        public ProfileController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia)
        {
            _SignInManager = sm;
            _UserManager = um;
            _mapper = mapper;
            _userDataAccess = ia;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var pvm = _mapper.Map<ProfileViewModel>(user);

            List<Item> ownedItems = _userDataAccess.GetItems(user.OwnerId);
            pvm.OwnerItems = _mapper.Map<List<ItemViewModel>>(ownedItems);

            return View(pvm);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(ProfileViewModel pvm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var itemToEdit = _userDataAccess.GetItem(user.OwnerId, pvm.EditItem);
            var itemView = _mapper.Map<ItemViewModel>(itemToEdit);
            return View(itemView);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> SubmitEdit(ItemViewModel ivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var item = _mapper.Map<Item>(ivm);
            var itemToEdit = _userDataAccess.EditItem(user.OwnerId, item);
            return RedirectToAction("IndeX", "Profile");
        }
    }
}
