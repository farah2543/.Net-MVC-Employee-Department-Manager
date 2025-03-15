using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }

}
