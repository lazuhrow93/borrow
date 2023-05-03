using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views.AddItem;
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
                viewModel.ItemsToSave.Add(viewModel.NewItemViewModel);
            }
            viewModel.NewItemViewModel = new();
            return View(viewModel);
        }
    }
}
