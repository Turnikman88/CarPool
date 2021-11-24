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
        public async Task<IActionResult> Index([FromQuery] int p)
        {
            var trips = await _trip.GetAsync(p);
            var model = new TripViewModel()
            {
                Trips = trips
            };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View(new TripViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripViewModel obj)
        {
            var driver = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;


            await _trip.PostAsync(new TripDTO()
            {
                AdditionalComment = obj.AdditionalComment,
                DepartureTime = obj.Date,
                DestinationAddressCity = obj.ToAddress,
                StartAddressCity = obj.FromAddress,
                DriverId = driver.Id.ToString(),
                FreeSeats = obj.FreeSeats
            });

            var trips = new TripViewModel { Trips = await _trip.GetAsync(0) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", trips.Trips, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var getUserEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            await _trip.JoinTrip(id, getUserEmail);

            var trips = new TripViewModel { Trips = await _trip.GetAsync(0) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", trips.Trips, true) });
        }
    }
}
