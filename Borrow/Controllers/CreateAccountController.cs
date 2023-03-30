using Borrow.Data;
using Borrow.Models;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    public class CreateAccountController : Controller
    {
        private BorrowContext _context { get; set; }

        public CreateAccountController(BorrowContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertUser(User newUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newUser);
            }
            _context.SaveChanges();
            return View("Successful");
        }
    }
}
