using System.Collections.Generic;
using Business.Interfaces;
using Common.Model;
using Common.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.APIs.Account
{
    [Route("api/account/register")]
    public class RegisterController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMessageService _messageService;

        public RegisterController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IMessageService messageService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._messageService = messageService;
        }
       
        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]RegisterUserVM newuser)
        {
            var response = new ApiResponse();

            if (newuser.Password != newuser.ConfirmPassword)
            {
                response.IsSucced = false;                
                response.Errors.Add("Password don't match");
                return new JsonResult(response);

            }

            var newUser = new ApplicationUser 
            {
                UserName = newuser.Email,
                Email = newuser.Email
            };

            var userCreationResult = _userManager.CreateAsync(newUser, newuser.Password);
            response.IsSucced = userCreationResult.Result.Succeeded;
            if (userCreationResult.Result.Succeeded)
            {
                response.Redirect.Redirected = true;
                response.Redirect.RedirectLink = "/Home/Index";
                return new JsonResult(response);

            }
            else
            {

                foreach (var error in userCreationResult.Result.Errors)
                    response.Errors.Add(error.Description); 
                return new JsonResult(response);

            }
           
            // var emailConfirmationToken =  _userManager.GenerateEmailConfirmationTokenAsync(newUser);
           // var tokenVerificationUrl = Url.Action("VerifyEmail", "Account", new {id = newUser.Id, token = emailConfirmationToken}, Request.Scheme);

          //  _messageService.Send(model.Email, "Verify your email", $"Click <a href=\"{tokenVerificationUrl}\">here</a> to verify your email");

        }

        
    }
}