using CarPool.Common;
using CarPool.Services.Contracts;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.ViewModels.DTOs;
using CarPool.Web.ViewModels.Mappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IBanService _ban;
        private readonly IMailService _mail;

        public AuthController(IAuthService auth,
            IApplicationUserService us,
            IAddressService ads,
            IBanService ban,
            IMailService mail)
        {
            this._auth = auth;
            this._us = us;
            this._ads = ads;
            this._ban = ban;
            _mail = mail;
        }



        [AllowAnonymous, Route("account/google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [AllowAnonymous, Route("account/google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            var email = claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            if (await _auth.IsExistingAsync(email))
            {
                await SignInWithRoleAsync(email, GlobalConstants.UserRoleName);
                return this.RedirectToAction("index", "home");
            }

            return this.RedirectToAction("GoogleSignUp", "Auth");
            //return Json(claims);
        }


        [HttpGet]
        public IActionResult GoogleSignUp()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> GoogleSignUp(GoogleRegisterDTO model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var userData = GetGoogleData(result);

            userData.AddressId = await _ads.AddressToId(new AddressDTO
            {
                StreetName = model.Address,
                CountryName = model.Country,
                CityName = model.City
            });

            userData.PhoneNumber = model.PhoneNumber;

            var toUser = userData.GetDTO();

            await this._us.PostAsync(toUser);

            await _mail.SendEmailAsync(new MailDTO { Reciever = userData.Email });
            return this.RedirectToAction("index", "home");
        }

        //[HttpPost]
        //public async Task<IActionResult> GoogleRegistration(RegisterDTO model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }
        //
        //    model.AddressId = await _ads.AddressToId(new AddressDTO
        //    {
        //        StreetName = model.Address,
        //        CountryName = model.Country,
        //        CityName = model.City
        //    });
        //
        //
        //    var toCustomer = model.GetDTO();
        //    await this._us.PostAsync(toCustomer);
        //
        //    await _mail.SendEmailAsync(new MailDTO { Reciever = model.Email });
        //    return this.Redirect(nameof(Login));
        //}

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

            return this.RedirectToAction("index", "home");
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
        //[Authorize(Roles = GlobalConstants.NotConfirmedRoleName)]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var email = await _auth.ConfirmEmail(token);
            if (email != null)
            {
                //here must change cookie role claim to user
                await this.SignInWithRoleAsync(email, GlobalConstants.UserRoleName);
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

            model.AddressId = await _ads.AddressToId(new AddressDTO
            {
                StreetName = model.Address,
                CountryName = model.Country,
                CityName = model.City
            });


            var toCustomer = model.GetDTO();
            await this._us.PostAsync(toCustomer);

            await _mail.SendEmailAsync(new MailDTO { Reciever = model.Email });
            return this.Redirect(nameof(Login));
        }
        private RegisterDTO GetGoogleData(AuthenticateResult result)
        {
            var claims = result.Principal.Identities.FirstOrDefault()
                            .Claims.Select(claim => new
                            {
                                claim.Issuer,
                                claim.OriginalIssuer,
                                claim.Type,
                                claim.Value
                            });

            var email = claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();
            var password = claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
            var firstName = claims.Where(c => c.Type == ClaimTypes.GivenName)
                   .Select(c => c.Value).SingleOrDefault();
            var lastName = claims.Where(c => c.Type == ClaimTypes.Surname)
                   .Select(c => c.Value).SingleOrDefault();
            var username = email.Split('@');
            var model = new RegisterDTO
            {
                Email = email,
                Password = $"User#{password}",
                FirstName = firstName,
                LastName = lastName,
                ConfirmPassword = $"User#{password}",
                Username = username[0]

            };
            return model;
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
