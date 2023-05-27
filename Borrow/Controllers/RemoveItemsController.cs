using AutoMapper;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Borrow.Controllers
{
    [Authorize]
    public class RemoveItemsController
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;

        public RemoveItemsController(SignInManager<User> sm, UserManager<User> um, IMapper mapper, IUserDataAccess ia)
        {
            _SignInManager = sm;
            _UserManager = um;
            _mapper = mapper;
            _userDataAccess = ia;
        }

        public ActionResult Index()
        {

            throw new NotImplementedException();
        }
    }
}
