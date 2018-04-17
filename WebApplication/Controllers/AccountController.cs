using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Register()
        {
            return
            View();
        }
    }
}