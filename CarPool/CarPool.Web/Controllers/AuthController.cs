using CarPool.Common;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.ViewModels.DTOs;
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
        private readonly IApplicationUserService _us;
        private readonly IAddressService _ads;

        public AuthController(IAuthService auth, IApplicationUserService us, IAddressService ads)
        {
            _auth = auth;
            this._us = us;
            this._ads = ads;
        }

        public IActionResult Login()
        {
            return this.View(new RequestAuthDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(RequestAuthDTO model)
        {
            if (! await _auth.IsPasswordValidAsync(model.Email, model.Password))
            {
                this.ModelState.AddModelError("Password", GlobalConstants.WRONG_CREDENTIALS);
                return this.View(model);
            }
            var loggedUserResult = await _auth.GetByEmailAsync(model.Email);

            if (loggedUserResult.Message != null)
            {
                this.ModelState.AddModelError("Password", GlobalConstants.TRIP_USER_BLOCKED_JOIN);
                return this.View(model);
            }

            await SignInWithRoleAsync(model.Email, loggedUserResult.Role);

            return this.RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new ApplicationUserDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (await _auth.IsExistingAsync(model.Email))
            {
                this.ModelState.AddModelError("Email", GlobalConstants.USER_EXISTS);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            //var toCustomer = model.GetCustomerDTO();
           // await this._us.PostAsync(toCustomer);
            return this.Redirect(nameof(Login));
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
