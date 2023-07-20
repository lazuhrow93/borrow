using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Borrow.Models;
using Microsoft.AspNetCore.Authorization;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Views.TableViews;
using Microsoft.Build.Framework;
using Borrow.Models.Views.TableViews.Create;

namespace Borrow.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IMasterDL _masterDL;
        private readonly RequestBusinessLogic RBL;

        public RequestController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            _userManager = um;
            _signInManager = sm;
            _mapper = mapper;
            _userDataAccess = ia;
            _masterDL  = masterDL;
            RBL = new(masterDL, _mapper);
        }

        [HttpGet]
        public async Task<ActionResult> RequestItem(int itemId)
        {
            CreateRequestViewModel crvm = new();
            crvm.Init(_masterDL.ItemDataLayer.Get(itemId));
            return View(crvm);
        }

        [HttpPost]
        public async Task<ActionResult> RequestItem(CreateRequestViewModel crvm)
        {
            var user = await _userManager.GetUserAsync(this.User);
            RBL.CreateRequest(crvm.ItemId, crvm.RequestType, crvm.RequestRate, crvm.ReturnDateUtc, user);
            return RedirectToAction("Index", "Home"); //TODO: Redirect to the current outgoing requests
        }

        [HttpGet]
        public async Task<IActionResult> IncomingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var ubrvm = new UserBorrowRequestsViewModel();
            ubrvm.Outgoing = RBL.GetOutGoing(user).ToList();
            ubrvm.Incoming = RBL.GetIncoming(user).ToList();
            
            return View(ubrvm);
        }

        [HttpGet]
        public async Task<IActionResult> OutgoingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var ubrvm = new UserBorrowRequestsViewModel();
            ubrvm.Outgoing = RBL.GetOutGoing(user).ToList();
            ubrvm.Incoming = RBL.GetIncoming(user).ToList();
            return View(ubrvm);
        }

        [HttpGet]
        public IActionResult ViewRequestInfo(int requestId)
        {
            var rvm = RBL.GetRequest(requestId);
            if (rvm == null) throw new Exception("OOPS!");
            RBL.UpdateStatus(requestId, Borrow.Models.Backend.Request.RequestStatus.Viewed);
            return View(rvm);
        }

        [HttpGet]
        public IActionResult AcceptRequest(int requestId)
        {
            var rvm = RBL.GetRequest(requestId);
            return View(rvm);
        }

        [HttpPost]
        public IActionResult ConfirmRequest(int requestId)
        {
            RBL.OwnerAcceptRequest(requestId);
            var rvm = RBL.GetRequest(requestId);
            return View("MeetupSpot", rvm);
        }

        [HttpPost]
        public IActionResult DeclineRequest(int requestId)
        {
            RBL.DeclineRequest(requestId);
            return View(requestId);
        }

        [HttpGet]
        public IActionResult MeetupSpot(int requestId)
        {
            RBL.OwnerAcceptRequest(requestId);
            var rvm = RBL.GetRequest(requestId);
            return View(rvm);
        }
    }
}
