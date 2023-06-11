using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    [Authorize]
    public class AddItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private UserManager<User> _UserManager;

        public AddItemController(UserManager<User> um, IMapper mapper, IUserDataAccess ia)
        {
            _UserManager = um;
            _mapper = mapper;
            _userDataAccess = ia;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AddItemViewModel setup = new();
            setup.ItemsToSave = new List<NewItemViewModel>();
            return View(setup);
        }

        [HttpPost]
        public IActionResult Index(AddItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ItemsToSave is null) viewModel.ItemsToSave = new List<NewItemViewModel>();
                viewModel.ItemsToSave.Add(viewModel.NewItemViewModel);
            }
            //viewModel.NewItemViewModel = new();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddInventory(AddItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.GetUserAsync(this.User);
                var p = _userDataAccess.GetAppProfile(user);
                var items = _mapper.Map<List<Item>>(viewModel.ItemsToSave);
                _userDataAccess.InsertItem(p, items);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromPending(AddItemViewModel viewModel)
        {
            viewModel.Remove(viewModel.IndexToRemove);
            return View("Index", viewModel);
        }
    }
}
