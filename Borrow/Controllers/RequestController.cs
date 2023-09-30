using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Borrow.Models.Views.Requests;
using Borrow.Data.BusinessLayer;
using Borrow.Models.Backend;

namespace Borrow.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly RequestBusinessLogic RBL;
        private readonly ListingsBusinessLogic LBL;
        private readonly ItemBusinessLogic IBL;

        public RequestController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IMasterDL masterDL)
        {
            _userManager = um;
            _signInManager = sm;
            _mapper = mapper;
            RBL = new(masterDL, _mapper);
            LBL = new(masterDL, _mapper);
            IBL = new(masterDL, _mapper);
        }

        [HttpGet]
        public async Task<ActionResult> SubmitRequestForListing(int ListingId)
        {
            var user = await _userManager.GetUserAsync(this.User);
            var rvm = RBL.GetCreateRequestViewModel(ListingId, user);
            return View(rvm);
        }

        [HttpPost]
        public async Task<ActionResult> SubmitRequestForListing(CreateRequestViewModel crvm)
        {
            RBL.CreateRequest(crvm);
            return RedirectToAction("OutgoingRequests", "Request");
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var incoming = RBL.GetIncomingRequestsViewModel(user);
            return View(incoming);
        }

        [HttpGet]
        public async Task<IActionResult> GetOutgoingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var outgoing = RBL.GetOutgoingRequestsViewModel(user);
            return View(outgoing);
        }
    }
}
