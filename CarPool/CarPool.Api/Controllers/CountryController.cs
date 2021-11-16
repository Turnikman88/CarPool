﻿using CarPool.Services.Data.Contracts;
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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _cs;

        public CountryController(ICountryService cs)
        {
            _cs = cs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountriesAsync(int page)
        {
            return this.Ok(await _cs.GetAsync(page));
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        //[Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CountryDTO>> GetCountryByIdAsync(int id)
        {
            var response = await _cs.GetCountryByIdAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }
            return this.NotFound(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        //[Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CountryDTO>> GetCountryByNameAsync(string name)
        {
            var response = await _cs.GetCountryByNameAsync(name);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }
            return this.NotFound(new { ErrorMessage = response.ErrorMessage});
        }

        [HttpGet("partname/{part}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        //[Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountryByNamePartAsync(int page, string part)
        {
            return this.Ok(await _cs.GetCountriesByPartNameAsync(page, part));
        }
    }
}
