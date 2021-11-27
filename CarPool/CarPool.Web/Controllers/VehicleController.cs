using CarPool.Services.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IUserVehicleService _vs;

        public VehicleController(IUserVehicleService vs)
        {
            this._vs = vs;
        }

        public async Task<IActionResult> Index()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var vehicle = await _vs.GetUserVehicle(email);

            return View(vehicle);
        }
    }
}
