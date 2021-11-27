using CarPool.Services.Data.Contracts;
using CarPool.Web.Infrastructure.Extensions;
using CarPool.Web.ViewModels.DTOs;
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
            var model = new UserVehicleViewModel
            {
                Id = vehicle.Id,
                CarModel = vehicle.Model,
                Color = vehicle.Color,
                FuelConsumptionPerHundredKilometers = vehicle.FuelConsumptionPerHundredKilometers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id, UserVehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "Index", model, false) });
            }
            return Json(new { isValid = true, html = "" });
        }
    }
}
