using System.Collections.Generic;
using Common.Model;
using Common.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.APIs.Account
{
    [Route("api/account/logout")]

    public class LogoutController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutController(SignInManager<ApplicationUser> signInManager)
        {
            this._signInManager = signInManager; 
            
        }
        [HttpGet]
        public JsonResult Logout()
        {
            var resp = _signInManager.SignOutAsync();
            ApiResponse response = new ApiResponse()
            {
                IsSucced = resp.IsCompleted,                                
            };
            if (resp.IsCompleted)
            {
                response.Redirect.Redirected = true;
                response.Redirect.RedirectLink = "/Home/Index/";
            }
            else
            {
                response.Errors.Add("your signout action has not completed.\nIt maybe has been because of our serer action.");
            }

            return new JsonResult(response);
        }    
    }
}