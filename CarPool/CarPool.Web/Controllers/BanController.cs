using CarPool.Services.Data.Contracts;
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

        public IActionResult Index()
        {
            return this.View(); 
        }

/*        [HttpPost]
        public async Task<IActionResult> Index()
        {

        }*/
    }
}
