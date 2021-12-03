using CarPool.Services.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.Web.Controllers
{
    public class BanController : Controller
    {
        private readonly IBanService _ban;
        private readonly IApplicationUserService _us;

        public BanController(IBanService ban, IApplicationUserService us)
        {
            _ban = ban;
            _us = us;
        }

        public IActionResult Index()
        {
            return View(); 
        }

/*        [HttpPost]
        public async Task<IActionResult> Index()
        {

        }*/
    }
}
