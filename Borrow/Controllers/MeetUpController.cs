using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Borrow.Controllers
{
    [Authorize]
    public class MeetUpController : Controller
    {
        [HttpGet]
        public IActionResult MeetUpSpot()
        {
            return View();
        }
    }
}
