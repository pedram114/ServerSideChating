using Business.Interfaces;
using Common.Model;
using Common.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.APIs.Account
{
    [Route("api/account/login")]
    public class LoginController : Controller
    {
        
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
    

        public LoginController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IMessageService messageService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;         
        }
        
        
        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]LoginUserVM loginuser)
        {
            var response = new ApiResponse();
            var user = _userManager.FindByEmailAsync(loginuser.Email);
            if (user == null)
            {
                response.IsSucced = false;                
                response.Errors.Add("Username or password is incorrect!");
                return new JsonResult(response);
            }
//            if (!user.EmailConfirmed)
//            {
//                ModelState.AddModelError(string.Empty, "Confirm your email first");
//                return View();
//            }

            var passwordSignInResult =  _signInManager.PasswordSignInAsync(loginuser.Email, loginuser.Password, isPersistent: loginuser.RememberMe, lockoutOnFailure: false);
            response.IsSucced = passwordSignInResult.Result.Succeeded;
            if (passwordSignInResult.Result.Succeeded)
            {
                response.Redirect.Redirected = true;
                response.Redirect.RedirectLink = "/Home/Index";
                return new JsonResult(response);
            }
            else
            {
                response.Errors.Add("Username or password is incorrect!");
                return new JsonResult(response);
            }
         
        }
    }
}