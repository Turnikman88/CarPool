using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly IBanService _bs;

        public BanController(IBanService bs)
        {
            _bs = bs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ApplicationUserDisplayDTO>>> GetAllBannedUsersAsync(int page)
        {
            return this.Ok(await _bs.GetAllBannedUsersAsync(page));
        }

        [HttpPatch("{email}/{days?}")]
        public async Task<ActionResult<BanDTO>> BanUserAsync(string email, string reason, string days)
        {
            var response = await _bs.BanUserAsync(email, reason, days);

            if(response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(response.ErrorMessage);
        }

        [HttpPatch("{email}/unban")]
        public async Task<ActionResult<BanDTO>> UnbanUserAsync(string email)
        {
            var response = await _bs.UnbanUserAsync(email);

            if (response.ErrorMessage is null)
            {
                return Ok(response.BanRemovedMessage);
            }

            return BadRequest(response.ErrorMessage);
        }
    }
}
