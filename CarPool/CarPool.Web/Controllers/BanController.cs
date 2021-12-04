using CarPool.Services.Contracts;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
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
        private readonly IMailService _ms;

        public BanController(IBanService ban, IApplicationUserService us, IMailService ms)
        {
            this._ban = ban;
            this._us = us;
            this._ms = ms;
        }

        public async Task<IActionResult> Index()
        {
            var reported = await _ban.GetTopReportedUsersAsync();

            return this.View(reported); 
        }


        [HttpPost]
        public async Task<IActionResult> Banned(int p)
        {
            var banned = await _ban.GetAllBannedUsersAsync(p);
            var model = new BanViewModel 
            {
                Banned = banned,
                CurrentPage = p,
                MaxPages = await _ban.GetPageCountAsync()
            };
            return Json(new {html = await Helper.RenderViewAsync(this, "_Banned", model, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Reported()
        {
            var reported = await _ban.GetTopReportedUsersAsync();

            return Json(new { html = await Helper.RenderViewAsync(this, "_Reported", reported, true) });
        }


        public async Task<IActionResult> Ban(string email)
        {
            var model = await _ban.GetReportedUserByEmailAsync(email);
            TempData.Put("ReportedUser", model);
            /*            return Json(new { html = await Helper.RenderViewAsync(this, "Ban", new ReportedDTO(), false) }); */
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ban(ReportedDTO model)
        {
            var temp = TempData.Get<ReportedDTO>("ReportedUser");

            await _ban.BanUserAsync(temp.Email, temp.Reason, model.Days);

            var reported = await _ban.GetTopReportedUsersAsync();

            var message = $"Due: {model.Days} Reasnons: {temp.Reason}";
            await _ms.SendEmailAsync(new MailDTO { IsBan = true, Reciever = temp.Email, Message = message });

            return Json(new {isValid = true,  html = await Helper.RenderViewAsync(this, "_Reported", reported, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Ignore(string email)
        {

            await _ban.IgnoreReportAsync(email);

            var reported = await _ban.GetTopReportedUsersAsync();

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Reported", reported, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Unban(string email)
        {

            await _ban.UnbanUserAsync(email);

            var banned = await _ban.GetAllBannedUsersAsync(0);
            var model = new BanViewModel { Banned = banned };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Banned", model, true) });
        }
    }
}
