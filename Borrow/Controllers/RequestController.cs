using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Borrow.Models.Views.Requests;
using Borrow.Models.Backend;
using Borrow.Data.Services.Interfaces;

namespace Borrow.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IUserService _userService;

        public RequestController(
            UserManager<User> um, 
            IRequestService requestService,
            IUserService userService)
        {
            _userService = userService;
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult> SubmitRequestForListing(int ListingId)
        {
            var user = await _userService.GetCurrentUser(this.User);
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
            var user = await _userService.GetCurrentUser(this.User);
            var incoming = _requestService.GetIncomingRequestsViewModel(user);
            return View(incoming);
        }

        [HttpGet]
        public async Task<IActionResult> OutGoingRequests()
        {
            var user = await _userService.GetCurrentUser(this.User);
            var outgoing = _requestService.GetOutgoingRequestsViewModel(user);
            return View(outgoing);
        }

        [HttpGet]
        public async Task<IActionResult> ViewIncomingRequest(int requestId)
        {
            var request = _requestService.GetRequestViewModel(requestId);
            _requestService.UpdateStatus(requestId, Models.Backend.Request.RequestStatus.Viewed);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> ViewOutgoingRequest(int requestId)
        {
            var request = _requestService.GetRequestViewModel(requestId);
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
            return View(_requestService.GetSetupMeetingViewModel(requestId));
        }

        [HttpPost]
        public async Task<IActionResult> RequesterSetupMeeting(SetupMeetingViewModel info)
        {
            _requestService.OfferMeetupTime(info);
            return RedirectToAction("OutGoingRequests");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptMeetupSpot(int requestId)
        {
            _requestService.ConfirmMeetupTime(requestId);
            var rvm = _requestService.GetRequestViewModel(requestId);
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
            var rvm = _requestService.GetRequestViewModel(requestId);
            return View(rvm);
        }
    }
}
