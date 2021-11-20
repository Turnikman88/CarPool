using CarPool.Common;
using CarPool.Services.Contracts;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.ViewModels.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{

    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IApplicationUserService _us;
        private readonly IAddressService _ads;
        private readonly IBanService _ban;
        private readonly IMailService _mail;

        public AuthController(IAuthService auth,
            IApplicationUserService us,
            IAddressService ads,
            IBanService ban)
        {
            this._auth = auth;
            this._us = us;
            this._ads = ads;
            this._ban = ban;
        }

        public IActionResult Login()
        {
            return this.View(new RequestAuthDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(RequestAuthDTO model)
        {
            if (!await _auth.IsPasswordValidAsync(model.Email, model.Password))
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

        // TODO: Move in admin panel
        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Ban()
        {
            return View(new BanUserDTO());
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Ban(string email, string reason, System.DateTime? days)
        {
            await _ban.BanUserAsync(email, reason, days);
            return this.View(); // return json with model 
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.NotConfirmedRoleName)]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            if (await _auth.ConfirmEmail(token))
            {
                return this.RedirectToAction("index", "home");
            }

            return this.RedirectToAction("error", "home");
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
            //await this._us.PostAsync(toCustomer);

            await _mail.SendEmailAsync(new MailDTO { Reciever = model.Email });
            return this.Redirect(nameof(Login));
        }

        private async Task SignInWithRoleAsync(string email, string userRoleName)
        {
            //You can add more claims as you wish but keep these KEYS here as is
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, email));
            //identity.AddClaim(new Claim(ClaimTypes.Role, userRoleName));
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, email));


            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
