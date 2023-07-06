﻿using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Borrow.Models;
using Microsoft.AspNetCore.Authorization;
using Borrow.Data.DataAccessLayer;
using Borrow.Models.Views.TableViews;

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
            RequestViewModel rvm = new();
            rvm.Initialize(_masterDL.ItemDataLayer.Get(itemId));
            return View(rvm);
        }

        [HttpPost]
        public async Task<ActionResult> RequestItem(RequestViewModel rvm)
        {
            var user = await _userManager.GetUserAsync(this.User);
            RBL.CreateRequest(rvm.ItemId, rvm.RequestType, rvm.RequestRate, rvm.ReturnDate, user);
            return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> ViewRequestInfo(int requestId)
        {
            var brvm = RBL.GetRequest(requestId);
            if (brvm == null) throw new Exception("OOPS!");
            RBL.UpdateStatus(requestId, Models.Backend.Request.RequestStatus.Viewed);
            return View(brvm);
        }
    }
}