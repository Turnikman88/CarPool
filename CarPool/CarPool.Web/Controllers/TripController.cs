using CarPool.Common;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.Infrastructure.Extensions;
using CarPool.Web.ViewModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.UserRoleName + "," + GlobalConstants.AdministratorRoleName)]
    public class TripController : Controller
    {
        private readonly ITripService _trip;
        private readonly IApplicationUserService _user;
        private readonly IRatingService _rating;

        public TripController(ITripService trip, IApplicationUserService user, IRatingService rating)
        {
            _trip = trip;
            _user = user;
            _rating = rating;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips = await _trip.GetAllUpcomingTripsAsync(0);
            var maxpages = await _trip.GetPageCountAsync();

            var model = new TripViewModel()
            {
                CurrentPage = 0,
                MaxPages = maxpages,
                UpcomingTrips = trips,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] int p)
        {
            var trips = await _trip.GetAllUpcomingTripsAsync(p);
            var maxpages = await _trip.GetPageCountAsync();

            var model = new TripViewModel()
            {
                CurrentPage = p,
                MaxPages = maxpages,
                UpcomingTrips = trips
            };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableTrips", model, true) });
        }

        [HttpGet]
        public async Task<IActionResult> MyTrips()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var trips = await _trip.GetUpcomingTripsByUserAsync(0, userEmail);
            var pasttrips = await _trip.GetPastByUserTrips(0, userEmail);
            var maxpages = await _trip.GetPageCountPerUserAsync(userEmail);

            var model = new TripViewModel()
            {
                UpcomingTrips = trips,
                PastTrips = pasttrips,
                CurrentPage = 0,
                MaxPages = maxpages

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MyTrips([FromQuery] int p)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var trips = await _trip.GetUpcomingTripsByUserAsync(p, userEmail);
            var pasttrips = await _trip.GetPastByUserTrips(p, userEmail);
            var maxpages = await _trip.GetPageCountPerUserAsync(userEmail);

            var model = new TripViewModel()
            {
                UpcomingTrips = trips,
                PastTrips = pasttrips,
                MaxPages = maxpages,
                CurrentPage = p
            };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableMyTrips", model, true) });
        }

        public async Task<IActionResult> Create()
        {
            var requestEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var driver = await _user.GetUserByEmailAsync(requestEmail);

            if (driver.HasVehicle) //TODO: redirect? 
            {
                return RedirectToAction("Index", "Vehicle");
            }
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

            var trips = new TripViewModel { UpcomingTrips = await _trip.GetUpcomingTripsByUserAsync(0, requestEmail) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_TableTrips", trips, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            await _trip.JoinTripAsync(id, userEmail);

            return await Index(0);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            await _trip.LeaveTripAsync(id, userEmail);

            return await MyTrips(0);

        }

        [HttpGet]
        public async Task<IActionResult> Rating()
        {
            return View(new RatingViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Rating(RatingViewModel obj, int id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var feedbackFromUser = await _user.GetUserByEmailAsync(userEmail);
            var trip = await _trip.GetTripByIDAsync(id);

            var response = await _rating.PostFeedbackAsync(
                new RatingDTO
                {
                    AddedByUserId = feedbackFromUser.Id,
                    ApplicationUserId = Guid.Parse(trip.DriverId),
                    Feedback = obj.Comment,
                    TripId = id,
                    Value = obj.Value
                });

            return await MyTrips(0);
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View(new RatingViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Report(RatingViewModel obj, int id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var feedbackFromUser = await _user.GetUserByEmailAsync(userEmail);
            var trip = await _trip.GetTripByIDAsync(id);

            var response = await _rating.PostFeedbackAsync(
                new RatingDTO
                {
                    AddedByUserId = feedbackFromUser.Id,
                    ApplicationUserId = Guid.Parse(trip.DriverId),
                    Feedback = obj.Comment,
                    TripId = id,
                    IsReport = true
                });

            return await MyTrips(0);
        }
    }
}
