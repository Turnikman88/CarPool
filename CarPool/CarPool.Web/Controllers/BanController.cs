using CarPool.Services.Data.Contracts;
using CarPool.Web.Infrastructure.Extensions;
using CarPool.Web.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    public class BanController : Controller
    {
        private readonly IBanService _ban;
        private readonly IApplicationUserService _us;

        public BanController(IBanService ban, IApplicationUserService us)
        {
            this._ban = ban;
            this._us = us;
        }

        public async Task<IActionResult> Index()
        {
            var reported = await _ban.GetAllReportedUsersAsync(0);

            return this.View(reported); 
        }

        [HttpPost]
        public async Task<IActionResult> Reported(int p)
        {
            var reported = await _ban.GetAllReportedUsersAsync(p);

            return Json(new { html = await Helper.RenderViewAsync(this, "_Reported", reported, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Banned(int p)
        {
            var banned = await _ban.GetAllBannedUsersAsync(p);

            return Json(new {html = await Helper.RenderViewAsync(this, "_Banned", banned, true) });
        }

        [HttpPost]
        public async Task<IActionResult> BanUser()
        {
            var reported = await _ban.BanUserAsync(email, days);

            return Json(new { html = await Helper.RenderViewAsync(this, "_Reported", reported, true) });
        }
    }
}
