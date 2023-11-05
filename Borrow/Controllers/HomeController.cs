﻿using AutoMapper;
using Borrow.Data.BusinessLayer;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models;
using Borrow.Models.Backend;
using Borrow.Models.Views.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Borrow.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly IMapper _mapper;
        private readonly IMasterDL _masterDl;
        private readonly INeighborhoodService _neighborhoodService;

        public HomeController(
            SignInManager<User> sm, 
            UserManager<User> um, 
            INeighborhoodService neighborhoodService,
            IMapper mapper)
        {
            userManager = um;
            signInManager = sm;
            _mapper = mapper;
            _neighborhoodService = neighborhoodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hvm = new HomeViewModel();
            if (signInManager.IsSignedIn(this.User))
            {
                var user = await userManager.GetUserAsync(this.User);
                hvm = _neighborhoodService.GetHomeViewModel(user);
            }
            return View(hvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}