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
        private readonly IRequestService _requestControllerService;
        private readonly IProfileService _profileControllerService;

        public RequestController(
            UserManager<User> um, 
            IRequestService requestService,
            IProfileService profileControllerService)
        {
            _profileControllerService = profileControllerService;
            _requestControllerService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult> SubmitRequestForListing(int ListingId)
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var rvm = _requestControllerService.GetCreateRequestViewModel(ListingId, user);
            return View(rvm);
        }

        [HttpPost]
        public async Task<ActionResult> SubmitRequestForListing(CreateRequestViewModel crvm)
        {
            _requestControllerService.CreateRequest(crvm);
            return RedirectToAction("OutgoingRequests", "Request");
        }

        [HttpGet]
        public async Task<IActionResult> IncomingRequests()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var incoming = _requestControllerService.GetIncomingRequestsViewModel(user);
            return View(incoming);
        }

        [HttpGet]
        public async Task<IActionResult> OutGoingRequests()
        {
            var user = await _profileControllerService.GetCurrentUser(this.User);
            var outgoing = _requestControllerService.GetOutgoingRequestsViewModel(user);
            return View(outgoing);
        }

        [HttpGet]
        public async Task<IActionResult> ViewIncomingRequest(int requestId)
        {
            var request = _requestControllerService.GetRequestViewModel(requestId);
            _requestControllerService.OwnerViewed(requestId);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> ViewOutgoingRequest(int requestId)
        {
            var request = _requestControllerService.GetRequestViewModel(requestId);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            _requestControllerService.AcceptRequest(requestId);
            return RedirectToAction("IncomingRequests");
        }

        [HttpPost]
        public async Task<IActionResult> DeclineRequest(int requestId)
        {
            _requestControllerService.DeclineRequest(requestId);
            return RedirectToAction("IncomingRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RequesterSetupMeeting(int requestId)
        {
            return View(_requestControllerService.GetSetupMeetingViewModel(requestId));
        }

        [HttpPost]
        public async Task<IActionResult> RequesterSetupMeeting(SetupMeetingViewModel info)
        {
            _requestControllerService.OfferMeetupTime(info);
            return RedirectToAction("OutGoingRequests");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptMeetupSpot(int requestId)
        {
            _requestControllerService.ConfirmMeetupTime(requestId);
            var rvm = _requestControllerService.GetRequestViewModel(requestId);
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
            var rvm = _requestControllerService.GetRequestViewModel(requestId);
            return View(rvm);
        }
    }
}
