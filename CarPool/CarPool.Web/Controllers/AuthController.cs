using CarPool.Common;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] RequestAuthDTO model)
        {
            if (! await _auth.IsPasswordValidAsync(model.Email, model.Password))
            {
                return BadRequest(GlobalConstants.WRONG_CREDENTIALS);
            }
            //TODO: SUBSTITUDE WITH YOUR OWN ACCOUNT-CHECKING METHOD
            var loggedUserResult = await _auth.GetByEmailAsync(model.Email);

            //Optional result checking for the account-checking method
            if (loggedUserResult.Message != null)
            {
                return BadRequest(loggedUserResult.Message);
            }

            //The real magic happens here
            await SignInWithRoleAsync(model.Email, loggedUserResult.Role);

            //Return whatever you want, does not matter (as long as its valid 200 response-type)
            return Json(new { Success = true, Message = "Successfull login" });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            //Return whatever you want, does not matter (as long as its valid 200 response-type)
            return Ok();
        }

       // [HttpGet]
       // [Authorize]
       // public async Task<IActionResult> ExpireCookies(string useremail)
       // {
       //
       // }

        private async Task SignInWithRoleAsync(string email, string userRoleName)
        {
            //You can add more claims as you wish but keep these KEYS here as is
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, email));
            identity.AddClaim(new Claim(ClaimTypes.Role, userRoleName));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
