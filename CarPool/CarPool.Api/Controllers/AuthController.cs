﻿using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ResponseAuthDTO>>> Login(RequestAuthDTO model)
        {
            return Ok(await _auth.AuthenticateAsync(model));
        }

       // [HttpPost]
       // [ProducesResponseType(200)]
       // [ProducesResponseType(401)]
       // [Authorize]
       // public async Task<ActionResult<IEnumerable<ResponseAuthDTO>>> Logout(RequestAuthDTO model)
       // {
       //     return this.Ok(await _auth.Authenticate(model));
       // }
    }
}
