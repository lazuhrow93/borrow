using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Borrow.Controllers
{
    [Authorize]
    public class RemoveItemsController : Controller
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;

        public RemoveItemsController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia)
        {
            _SignInManager = sm;
            _UserManager = um;
            _mapper = mapper;
            _userDataAccess = ia;
        }

        public async Task<ActionResult> Index()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var rivm = new RemoveItemsViewModel();
            var userItems = _userDataAccess.GetItems(user.OwnerId);
            var mappedItems = _mapper.Map<List<ItemViewModel>>(userItems);
            rivm.Items = mappedItems.Select(i=> { i.IsSelected = false; return i; }).ToList();
            return View(rivm);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Remove(RemoveItemsViewModel rivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var itemsToDelete = _mapper.Map<List<Item>>(rivm.Items.Where(i => i.IsSelected));
            _userDataAccess.DeleteItem(user.OwnerId, itemsToDelete.Select(i => i.Identifier).ToList());
            return RedirectToAction("Index", "Profile");
        }
    }
}
