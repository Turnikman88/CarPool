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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IBanService _ban;
        private readonly IMailService _mail;
        private readonly ICountryService _cs;
        private readonly IProfilePictureService _pps;
        private readonly IGoogleAccountService _gs;

        public AuthController(IAuthService auth,
            IApplicationUserService us,
            IAddressService ads,
            IBanService ban,
            IMailService mail,
            ICountryService cs,
            IProfilePictureService pps,
            IGoogleAccountService gs)
        {
            _auth = auth;
            _us = us;
            _ads = ads;
            _ban = ban;
            _mail = mail;
            _cs = cs;
            _pps = pps;
            _gs = gs;
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
                return RedirectToAction("index", "home");
            }

            return RedirectToAction("GoogleSignUp", "Auth");
            //return Json(claims);
        }

        [HttpGet]
        public async Task<IActionResult> GoogleSignUp()
        {
            var model = new GoogleRegisterDTO();
            model.Countries = await RenderCountries();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GoogleSignUp(GoogleRegisterDTO model)
        {
            model.Countries = await RenderCountries();

            if (!ModelState.IsValid)
            {
                return View(model);
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
            await _mail.SendEmailAsync(new MailDTO { Reciever = userData.Email });

            await _gs.AddGoogleAccount(userData.Email);

            await _us.PostAsync(toUser);


            ViewData["MessageSent"] = true;
            return View(model);
        }

        public IActionResult Login()
        {
            return View(new RequestAuthDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(RequestAuthDTO model)
        {
            if (!await _auth.IsPasswordValidAsync(model.Email, model.Password))
            {
                ModelState.AddModelError("Password", GlobalConstants.WRONG_CREDENTIALS);
                return View(model);
            }
            var user = await _auth.GetByEmailAsync(model.Email);

            if (user.Message != null)
            {
                ModelState.AddModelError("Password", GlobalConstants.TRIP_USER_BLOCKED_JOIN);
                return View(model);
            }

            await SignInWithRoleAsync(model.Email, user.Role);

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();  

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var email = await _auth.ConfirmEmail(token);
            if (email != null)
            {
                await SignInWithRoleAsync(email, GlobalConstants.UserRoleName);

                return RedirectToAction("index", "home");
            }

            return RedirectToAction("error", "home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterDTO();
            model.Countries = await RenderCountries();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            model.Countries = await RenderCountries();

            if (await _auth.IsExistingAsync(model.Email))
            {
                ModelState.AddModelError("Email", GlobalConstants.USER_EXISTS);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.AddressId = await _ads.AddressToId(new AddressDTO
            {
                StreetName = model.Address,
                CountryName = model.Country,
                CityName = model.City
            });


            var toUser = model.GetDTO();

            await _us.PostAsync(toUser);

            await _mail.SendEmailAsync(new MailDTO { Reciever = model.Email });

            ViewData["MessageSent"] = true;

            return View(model);
        }

        [HttpGet]
        public IActionResult UpdatePassword(string token)
        {
            var email = _auth.CheckConfirmTokenAndExtractEmail(token);
            if (email.Contains('@'))
            {
                TempData["Email"] = email;
                return View(new UpdatePasswordDTO());
            }
            return RedirectToAction("error", "home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO obj)
        {
            var email = TempData["Email"].ToString();
            await _us.UpdatePasswordAsync(email, obj.Password);
            ViewData["PasswordUpdated"] = true;
            return View(new UpdatePasswordDTO());
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.UserRoleName + "," + GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Settings()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _us.GetUserByEmailOrIdAsync(email);

            var model = user.GetDTO();

            var address = await _ads.GetAddressByIdAsync(user.AddressId);

            model.IsGoogleAccount = await _gs.IsGoogleAccount(email);
            TempData["IsGoogleAccount"] = model.IsGoogleAccount;

            model.Countries = await RenderCountries();

            model.Country = address.CountryName;
            model.City = address.CityName;
            model.Address = address.StreetName;
            model.ProfilePictureLink = await _pps.GetPictureLinkByUserEmail(email);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.UserRoleName + "," + GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Settings(SettingsDTO model)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (TempData["IsGoogleAccount"].ToString() == "False"
                && !await _auth.IsPasswordValidAsync(email, model.Password))
            {
                ModelState.AddModelError("Password", GlobalConstants.OLD_PASSWORD);
            }

            if (model.NewPassword != null)
            {
                model.Password = model.NewPassword;
            }

            if (!ModelState.IsValid)
            {
                TempData["IsGoogleAccount"] = model.IsGoogleAccount;

                model.Countries = await RenderCountries();
                return View(model);
            }

            if (model.ProfilePicture != null)
            {
                await _pps.UpdateAsync(email, model.ProfilePicture);
            }

            model.AddressId = await _ads.AddressToId(new AddressDTO
            {
                StreetName = model.Address,
                CountryName = model.Country,
                CityName = model.City
            });

            model.Role = role;
            var toUser = model.GetDTO();
            await _us.UpdateAsync(email, toUser);

            return RedirectToAction("index", "home");
        }

        [HttpDelete]
        [Authorize(Roles = GlobalConstants.UserRoleName + "," + GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteGoogleAccount()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (await _gs.IsGoogleAccount(email))
            {
                await _gs.DeleteGoogleAccount(email);
            }

            await _us.DeleteAsync(email);

            await HttpContext.SignOutAsync();

            return Ok();
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

        private async Task<List<SelectListItem>> RenderCountries()
        {
            var countries = await _cs.RenderCountryListAsync();

            var model = new List<SelectListItem>();

            foreach (var country in countries)
            {
                model.Add(new SelectListItem() { Text = country.Name, Value = country.Name });
            }

            return model;
        }
    }
}
