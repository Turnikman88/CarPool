using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.Infrastructure.Extensions;
using CarPool.Web.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripService _trip;
        private readonly IApplicationUserService _user;

        public TripController(ITripService trip, IApplicationUserService user)
        {
            _trip = trip;
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips = await _trip.GetAsync(0);
            var maxpages = await _trip.GetPageCountAsync();

            var model = new TripViewModel()
            {
                CurrentPage = 0,
                MaxPages = maxpages,
                Trips = trips
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] int p)
        {
            var trips = await _trip.GetAsync(p);
            var maxpages = await _trip.GetPageCountAsync();

            var model = new TripViewModel()
            {
                CurrentPage = p,
                MaxPages = maxpages,
                Trips = trips
            };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableTrips", model, true) });
        }

        [HttpGet]
        public async Task<IActionResult> MyTrips()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var trips = await _trip.GetTripsByUserAsync(0, userEmail);
            var maxpages = await _trip.GetPageCountPerUserAsync(userEmail);

            var model = new TripViewModel()
            {
                Trips = trips,
                CurrentPage = 0,
                MaxPages = maxpages

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MyTrips([FromQuery] int p)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var trips = await _trip.GetTripsByUserAsync(p, userEmail);
            var maxpages = await _trip.GetPageCountPerUserAsync(userEmail);

            var model = new TripViewModel()
            {
                Trips = trips,
                MaxPages = maxpages,
                CurrentPage = p
            };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableMyTrips", model, true) });
        }

        public async Task<IActionResult> Create()
        {
            return View(new TripViewModel() { Date = System.DateTime.Today });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripViewModel obj)
        {
            var requestEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var driver = await _user.GetUserByEmailAsync(requestEmail);

            await _trip.PostAsync(new TripDTO()
            {
                AdditionalComment = obj.AdditionalComment,
                DepartureTime = obj.Date,
                DestinationAddressCountry = obj.DestinationCountry,
                DestinationAddressCity = obj.DestinationCity,
                StartAddressCity = obj.OriginCity,
                StartAddressCountry = obj.OriginCountry,
                DriverId = driver.Id.ToString(),
                FreeSeats = obj.FreeSeats
            });

            var trips = new TripViewModel { Trips = await _trip.GetTripsByUserAsync(0, requestEmail) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableTrips", trips, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            await _trip.JoinTripAsync(id, userEmail);

            var trips = await _trip.GetTripsByUserAsync(0, userEmail);
            var maxpages = await _trip.GetPageCountPerUserAsync(userEmail);

            var model = new TripViewModel()
            {
                Trips = trips,
                CurrentPage = 0,
                MaxPages = maxpages
            };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableTrips", model, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            var asd = await _trip.LeaveTripAsync(id, userEmail);

            var trips = await _trip.GetTripsByUserAsync(0, userEmail);
            var maxpages = await _trip.GetPageCountPerUserAsync(userEmail);

            var model = new TripViewModel()
            {
                Trips = trips,
                CurrentPage = 0,
                MaxPages = maxpages
            };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableTrips", model, true) });
        }
    }
}
