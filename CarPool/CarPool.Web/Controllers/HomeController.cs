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
using System.Linq;
using System.Net;
using System.Security.Claims;
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
            _us = us;
            _ts = ts;
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var usersCount = await _us.UsersCountAsync();
            var tripsCount = await _ts.TripsCountAsync();
            var topUsers = await _us.TopUsersAsync();
            var model = new StatisticsViewModel
            {
                UsersCount = usersCount,
                TripsCount = tripsCount,
                TopUsers = topUsers
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Chat()
        {
            var requestEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            var trips = await _ts.GetUpcomingTripsByUserAsync(0, requestEmail);
            List<string> tripsids = new List<string>();

            foreach (var item in trips)
            {
                tripsids.Add($"{item.DepartureTime.Date.ToString("MMMM dd, yyyy")}/{item.StartAddressCity}/{item.DestinationAddressCity}");
            }
            var model = new ChatViewModel()
            {
                TripsIds = tripsids
            };


            return this.View(model);
        }

        public IActionResult Contact()
        {
            return View(new MailDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Contact(MailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Phone is null)
            {
                model.Phone = GlobalConstants.NOT_PROVIDED;
            }
            model.isFromContact = true;
            await _ms.SendEmailAsync(model);

            model.isSent = true;
            return View(model);
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
