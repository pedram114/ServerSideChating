using System.Collections.Generic;
using Business.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.APIs.Account
{
    [Route("api/account/")]
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageService _messageService;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, IMessageService messageService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._messageService = messageService;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody] RegisterUserVM newuser)
        {
            
            if (newuser.Password != newuser.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password don't match");
                return new JsonResult("");
            }

            var newUser = new IdentityUser 
            {
                UserName = newuser.UserName,
                Email = newuser.Email
            };

            var userCreationResult = _userManager.CreateAsync(newUser, newuser.Password);
            if (!userCreationResult.Result.Succeeded)
            {
                foreach(var error in userCreationResult.Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return new JsonResult("");
            }

           // var emailConfirmationToken =  _userManager.GenerateEmailConfirmationTokenAsync(newUser);
           // var tokenVerificationUrl = Url.Action("VerifyEmail", "Account", new {id = newUser.Id, token = emailConfirmationToken}, Request.Scheme);

          //  _messageService.Send(model.Email, "Verify your email", $"Click <a href=\"{tokenVerificationUrl}\">here</a> to verify your email");

            return new JsonResult("");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}