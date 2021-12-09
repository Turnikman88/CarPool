using CarPool.Common;
using CarPool.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.UserRoleName + "," + GlobalConstants.AdministratorRoleName)]
    public class InboxController : Controller
    {
        private readonly IInboxService _inbox;

        public InboxController(IInboxService inbox)
        {
            _inbox = inbox;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            var model = await _inbox.GetUserMessages(userEmail);
            return View(model);
        }
    }
}
