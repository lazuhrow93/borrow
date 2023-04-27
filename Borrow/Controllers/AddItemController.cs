using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    [Authorize]
    public class AddItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _uda;

        public AddItemController(IMapper mapper, IUserDataAccess ia)
        {
            _mapper = mapper;
            _uda = ia;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
