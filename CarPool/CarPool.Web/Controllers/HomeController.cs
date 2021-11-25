﻿using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Services.Contracts;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.ViewModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _ms;
        private readonly IApplicationUserService _us;
        private readonly ITripService _ts;

        public HomeController(IMailService ms,
            IApplicationUserService us,
            ITripService ts)
        {
            _ms = ms;
            this._us = us;
            this._ts = ts;
        }

        public IActionResult About()
        {
            return this.View();
        }
                
        public async Task<IActionResult> Index()
        {
            var usersCount = await _us.UsersCountAsync();
            var tripsCount = await _ts.TripsCountAsync();
            var model = new StatisticsViewModel
            {
                UsersCount = usersCount,
                TripsCount = tripsCount
            };
            return this.View(model);
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
            model.isFromContact = true;
            await _ms.SendEmailAsync(model);

            model.isSent = true;
            return this.View(model);
        }
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var imageLink = $"{GlobalConstants.Domain}/images/";

            if (exception != null)
            {
                switch (exception)
                {
                    case AppException e:
                        // custom application error
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        imageLink += "400.png";
                        break;
                    case UnauthorizedAppException e:
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        imageLink += "401.png";
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        imageLink += "404.png";
                        break;
                    default:
                        // unhandled error
                        imageLink += "500.png";
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
            else
            {
                imageLink += "404.png";                
            }

            var statuscode = HttpContext.Response.StatusCode;
            return View(new ErrorDTO { StatusCode = statuscode, Message = exception?.Message ?? "Wrong Address!", ImageLink = imageLink });
        }
    }
}
