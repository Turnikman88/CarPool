using CarPool.Services.Data.Contracts;
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

        /*[HttpPost]
        public async Task<IActionResult> Login([FromBody] GetLoggedUserInfoRequestModel requestModel)
        {
            //TODO: SUBSTITUDE WITH YOUR OWN ACCOUNT-CHECKING METHOD
            var loggedUserResult = await _auth.

            //Optional result checking for the account-checking method
            if (!loggedUserResult.IsSuccess)
            {
                return BadRequest(loggedUserResult.Message);
            }

            //The real magic happens here
            await SignInWithRoleAsync(requestModel.Username, loggedUserResult.UserRoleName);

            //Return whatever you want, does not matter (as long as its valid 200 response-type)
            return Json(new { Success = true, Message = "Successfull login" });
        }*/

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            //Return whatever you want, does not matter (as long as its valid 200 response-type)
            return Ok();
        }

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
