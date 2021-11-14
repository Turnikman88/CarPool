using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService _us;

        public UserController(IApplicationUserService us)
        {
            _us = us;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ApplicationUserDisplayDTO>>> FilterUsersAsync(int page, string part)
        {
            return this.Ok(await _us.FilterUsersAsync(page, part));
        }
    }
}
