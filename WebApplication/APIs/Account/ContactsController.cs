using Business.Interfaces;
using Common.Model;
using Common.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.APIs.Account
{
    
    [Route("api/account/contacts")]
    public class ContactsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
    

        public ContactsController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;         
        }
        
        // POST
        // POST api/values
        [HttpGet]
        public JsonResult Get()
        {
            var response = new ApiResponse {IsSucced = true};
            if (User.Identity.Name == "pedram.gilaniniya@mail.com")
            {
                response.Header = "pedram.gilaniniya@gmail.com";
                return new JsonResult(response);
            }
            else
            {
                response.Header = "pedram.gilaniniya@mail.com";
                return new JsonResult(response);
            }

            
        }
    }
}