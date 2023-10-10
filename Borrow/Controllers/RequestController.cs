using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Borrow.Models.Views.Requests;
using Borrow.Data.BusinessLayer;
using Borrow.Models.Backend;
using System.Threading.Tasks.Dataflow;

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
            return RedirectToAction("GetOutgoingRequests", "Request");
        }

        [HttpGet]
        public async Task<IActionResult> IncomingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var incoming = RBL.GetIncomingRequestsViewModel(user);
            return View(incoming);
        }

        [HttpGet]
        public async Task<IActionResult> OutGoingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var outgoing = RBL.GetOutgoingRequestsViewModel(user);
            return View(outgoing);
        }

        [HttpGet]
        public async Task<IActionResult> ViewIncomingRequest(int requestId)
        {

            var request = RBL.GetRequestViewModel(requestId);
            RBL.UpdateStatus(requestId, Models.Backend.Request.RequestStatus.Viewed);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            RBL.AcceptRequest(requestId);
            return View("RequestAccepted");
        }

        [HttpPost]
        public async Task<IActionResult> DeclineRequest(int requestId)
        {
            RBL.DeclineRequest(requestId);
            return View("RequestDeclined");
        }
    }
}
