using CarPool.API.Infrastructure.Attributes;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<ApplicationUserDisplayDTO>>> GetAsync(int page)
        {
            return this.Ok(await _us.GetAsync(page));
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ApplicationUserDisplayDTO>>> FilterUsersAsync(int page, string part)
        {
            return this.Ok(await _us.FilterUsersAsync(page, part));
        }

        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ApplicationUserDisplayDTO>> GetUserByEmailAsync(string email)
        {
            var response = await _us.GetUserByEmailAsync(email);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ApplicationUserDisplayDTO>> CreateUser(ApplicationUserDTO obj)
        {
            var response = await _us.PostAsync(obj);

            if (response.ErrorMessage is null)
            {
                return this.Created("Get", response);
            }

            return this.BadRequest(new { ErrorMessage = response.ErrorMessage });
        }
        
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ApplicationUserDisplayDTO>> UpdateAsync(Guid id, ApplicationUserDTO obj)
        {
            var response = await _us.UpdateAsync(id, obj);

            if (response.ErrorMessage is null)
            {
                return this.Created("Get", response);
            }

            return this.BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ApplicationUserDisplayDTO>> DeleteAsync(Guid id)
        {
            var response = await _us.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(new { ErrorMessage = response.ErrorMessage });
        }
    }
}
