﻿using AutoMapper;
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
        private readonly ListingsBusinessLogic LBL;

        public RequestController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia, IMasterDL masterDL)
        {
            _userManager = um;
            _signInManager = sm;
            _mapper = mapper;
            _userDataAccess = ia;
            _masterDL  = masterDL;
            RBL = new(masterDL, _mapper);
            LBL = new(masterDL, _mapper);
        }

        [HttpGet]
        public async Task<ActionResult> RequestItem(int itemId)
        {
            return View(new CreateRequestViewModel(LBL.GetItemById(itemId)));
        }

        [HttpPost]
        public async Task<ActionResult> RequestItem(CreateRequestViewModel crvm)
        {
            var user = await _userManager.GetUserAsync(this.User);
            RBL.CreateRequest(crvm.ItemInformation.ItemId, crvm.RequestType, crvm.RequestRate, crvm.ReturnDateUtc, user);
            return RedirectToAction("OutgoingRequests", "Request");
        }

        [HttpGet]
        public async Task<IActionResult> IncomingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            return View(new UserBorrowRequestsViewModel(RBL.GetOutgoing(user), RBL.GetIncoming(user)));
        }

        [HttpGet]
        public async Task<IActionResult> OutgoingRequests()
        {
            var user = await _userManager.GetUserAsync(this.User);
            return View(new UserBorrowRequestsViewModel(RBL.GetOutgoing(user), RBL.GetIncoming(user)));
        }

        [HttpGet]
        public IActionResult ViewRequestInfo(int requestId)
        {
            var requestItem = RBL.GetRequest(requestId);

            RBL.UpdateStatus(requestId, Borrow.Models.Backend.Request.RequestStatus.Viewed);
            return View(new RequestViewModel(requestItem.Request, requestItem.Item));
        }

        [HttpGet]
        public IActionResult AcceptRequest(int requestId)
        {
            var requestInformation = RBL.GetRequest(requestId);
            return View(new RequestViewModel(requestInformation.Request, requestInformation.Item));
        }

        [HttpPost]
        public IActionResult ConfirmRequest(int requestId)
        {
            RBL.OwnerAcceptRequest(requestId);
            var requestInformation = RBL.GetRequest(requestId);
            return View("MeetupSpot", new RequestViewModel(requestInformation.Request, requestInformation.Item));
        }

        [HttpGet]
        public IActionResult DeclineRequest(int requestId)
        {
            var requestInformation = RBL.GetRequest(requestId);
            return View(new RequestViewModel(requestInformation.Request, requestInformation.Item));

        }

        [HttpPost]
        public IActionResult ConfirmDeclineRequest(int requestId)
        {
            RBL.DeclineRequest(requestId);
            return RedirectToAction("IncomingRequests");
        }

        [HttpGet]
        public IActionResult MeetupSpot(int requestId)
        {
            var requestInformation = RBL.GetRequest(requestId);
            return View(new RequestViewModel(requestInformation.Request, requestInformation.Item));
        }
    }
}
