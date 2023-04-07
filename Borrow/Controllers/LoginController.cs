using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Borrow.Data;
using Borrow.Models.Identity;

namespace Borrow.Controllers
{
    public class LoginController : Controller
    {
        private readonly BorrowContext _context;

        public LoginController(BorrowContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(User user)
        {
            if (_context.UserNameExists(user))
            {
                
                return View("UserHome", _context.GetUser(user));
            }
            else
            {
                return View("Index");
            }
            
        }
    }
}
