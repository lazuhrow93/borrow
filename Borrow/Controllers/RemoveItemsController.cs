using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Views.Item;
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
            rivm.Items = _mapper.Map<List<ItemViewModel>>(userItems);
            return View(rivm);
        }

        [Authorize]
        public async Task<ActionResult> Remove(ProfileViewModel pvm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            int? indextoRemove = pvm.RemoveAtIndex;
            if (indextoRemove is null) return View();
            else if (indextoRemove < 0 || indextoRemove >= pvm.OwnerItems.Count()) return View();
            else
            {
                var itemDelete = pvm.OwnerItems[(int)indextoRemove];
                var ownerId = user.OwnerId;
                _userDataAccess.DeleteItem(ownerId, itemDelete.Identifier);
                pvm.RemoveFromProfile((int)pvm.RemoveAtIndex);
            }
            return View(pvm);
        }
    }
}
