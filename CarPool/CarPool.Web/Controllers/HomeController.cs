using CarPool.Common;
using CarPool.Services.Contracts;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _ms;
        public HomeController(IMailService ms)
        {
            _ms = ms;
        }
        public IActionResult Index()
        {   
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View(new MailDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Contact(MailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.Phone is null)
            {
                model.Phone = GlobalConstants.NOT_PROVIDED;
            }
            await _ms.SendEmailAsync(model);

            model.isSent = true;
            return this.View(model);
        }
    }
}
