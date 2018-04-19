using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult LoginRegister()
        {
            return
            View();
        }
        
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}