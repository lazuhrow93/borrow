using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
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
        public async Task<ActionResult> EditItem(ProfileViewModel pvm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var itemToEdit = _userDataAccess.GetItem(user.OwnerId, pvm.EditItem);
            var itemView = _mapper.Map<ItemViewModel>(itemToEdit);
            return View(itemView);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditItem(ItemViewModel ivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var item = _mapper.Map<Item>(ivm);
            var itemToEdit = _userDataAccess.EditItem(user.OwnerId, item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> ListUnlist()
        {
            var elvm = new EditListingsViewModel(_mapper);
            var user = await _UserManager.GetUserAsync(this.User);
            elvm.MapItems(_userDataAccess.GetItems(user.OwnerId));
            return View(elvm);
        }

        [HttpPost]
        public async Task<ActionResult> Unlist(Guid unlistItem)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var oldItem = _userDataAccess.GetItem(user.OwnerId, unlistItem);
            oldItem.Unlist();
            var newItem = oldItem;
            _userDataAccess.EditItem(user.OwnerId, newItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> List(Guid listItem)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var oldItem = _userDataAccess.GetItem(user.OwnerId, listItem);
            oldItem.List();
            var newItem = oldItem;
            _userDataAccess.EditItem(user.OwnerId, newItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Remove()
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var rivm = new RemoveItemsViewModel();
            var userItems = _userDataAccess.GetItems(user.OwnerId);
            var mappedItems = _mapper.Map<List<ItemViewModel>>(userItems);
            rivm.Items = mappedItems.Select(i => { i.IsSelected = false; return i; }).ToList();
            return View(rivm);

        }

        [HttpPost]
        public async Task<ActionResult> Remove(RemoveItemsViewModel rivm)
        {
            var user = await _UserManager.GetUserAsync(this.User);
            var itemsToDelete = _mapper.Map<List<Item>>(rivm.Items.Where(i => i.IsSelected));
            _userDataAccess.DeleteItem(user.OwnerId, itemsToDelete.Select(i => i.Identifier).ToList());
            return RedirectToAction("Index");

        }
    }
}
