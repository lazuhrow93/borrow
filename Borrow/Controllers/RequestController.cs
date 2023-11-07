using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Borrow.Models.Views.Requests;
using Borrow.Data.BusinessLayer;
using Borrow.Models.Backend;
using System.Threading.Tasks.Dataflow;
using Borrow.Data.Services.Interfaces;

namespace Borrow.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly RequestBusinessLogic RBL;
        private readonly IRequestService _requestService;

        public RequestController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IMasterDL masterDL, IRequestService requestService)
        {
            _userManager = um;
            _signInManager = sm;
            _mapper = mapper;
            RBL = new(masterDL, _mapper);
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult> SubmitRequestForListing(int ListingId)
        {
            var user = await _userManager.GetUserAsync(this.User);
            var rvm = _requestService.GetCreateRequestViewModel(ListingId, user);
            return View(rvm);
        }

        [HttpPost]
        public async Task<ActionResult> SubmitRequestForListing(CreateRequestViewModel crvm)
        {
            _requestService.CreateRequest(crvm);
            return RedirectToAction("OutgoingRequests", "Request");
        }

        [HttpGet]
        public async Task<IActionResult> IncomingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var incoming = _requestService.GetIncomingRequestsViewModel(user);
            return View(incoming);
        }

        [HttpGet]
        public async Task<IActionResult> OutGoingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var outgoing = _requestService.GetOutgoingRequestsViewModel(user);
            return View(outgoing);
        }

        [HttpGet]
        public async Task<IActionResult> ViewIncomingRequest(int requestId)
        {

            var request = RBL.GetRequestViewModel(requestId);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> ViewOutgoingRequest(int requestId)
        {

            var request = RBL.GetRequestViewModel(requestId);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            _requestService.AcceptRequest(requestId);
            return RedirectToAction("ViewIncomingRequest", requestId);
        }

        [HttpPost]
        public async Task<IActionResult> DeclineRequest(int requestId)
        {
            _requestService.DeclineRequest(requestId);
            return RedirectToAction("IncomingRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RequesterSetupMeeting(int requestId)
        {
            return View(RBL.GetSetupMeetingViewModel(requestId));
        }

        [HttpPost]
        public async Task<IActionResult> RequesterSetupMeeting(SetupMeetingViewModel info)
        {
            _requestService.SetUpMeetingSpot(info);
            return RedirectToAction("OutGoingRequests");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptMeetupSpot(int requestId)
        {
            _requestService.ConfirmMeetup(requestId);
            var rvm = RBL.GetRequestViewModel(requestId);
            return View("ViewMeetupSpot", rvm);
        }

        [HttpPost]
        public async Task<IActionResult> DeclineMeetupSpot(int requestId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> ViewMeetupSpot(int requestId)
        {
            var rvm = RBL.GetRequestViewModel(requestId);
            return View(rvm);
        }
    }
}
