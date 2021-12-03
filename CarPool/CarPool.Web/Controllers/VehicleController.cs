using CarPool.Services.Data.Contracts;
using CarPool.Web.Infrastructure.Extensions;
using CarPool.Web.ViewModels.DTOs;
using CarPool.Web.ViewModels.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IUserVehicleService _vs;
        private readonly IAuthService _auth;

        public VehicleController(IUserVehicleService vs, IAuthService auth)
        {
            _vs = vs;
            _auth = auth;
        }

        public async Task<IActionResult> Index()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var vehicle = await _vs.GetUserVehicle(email);

            return View(vehicle.GetViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id, UserVehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Index", model, false) });
            }

            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var modelDTO = model.GetDto();

            modelDTO.ApplicationUserId = await _auth.GetUserId(email);

            await _vs.UpdateAsync(id, modelDTO);

            return Json(new { isValid = true, html = "" });
        }
    }
}
