using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class ChatController : Controller
    {
        // GET
        public IActionResult startnewchat()
        {
            return
            View();
        }
    }
}