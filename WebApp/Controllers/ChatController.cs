using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    
        [HttpGet]
        public ActionResult SendMessage(string fromId,string ToId,string Message)
        {
            return null;
        }
    }
}