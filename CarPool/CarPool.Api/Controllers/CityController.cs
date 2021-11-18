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
    public class CityController : ControllerBase
    {
        private readonly ICityService _cs;

        public CityController(ICityService cs)
        {
            _cs = cs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesAsync(int page)
        {
            return this.Ok(await _cs.GetAsync(page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<CityDTO>> GetCityByIdAsync(int id)
        {
            var response = await _cs.GetCityByIdAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }
            return this.NotFound(response);
        }

        [HttpGet("byname/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<CityDTO>> GetCityByNameAsync(string name)
        {
            var response = await _cs.GetCityByNameAsync(name);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }
            return this.NotFound(response);
        }

        [HttpGet("bypart/{part}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesByPartNameAsync(int page, string part)
        {
            return this.Ok(await _cs.GetCitiesByPartNameAsync(page, part));
        }

        [HttpGet("bycountry/{countryname}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesByCountryNameAsync(int page, string countryname)
        {
            return this.Ok(await _cs.GetCitiesByCountryNameAsync(page, countryname));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<CityDTO>> CreateCityAsync(CityDTO obj)
        {
            var response = await _cs.PostAsync(obj);

            if (response.ErrorMessage is null)
            {
                return this.Created("Get", response);
            }
            return this.NotFound(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<CityDTO>> UpdateCityAsync(int id, CityDTO obj)
        {
            var response = await _cs.UpdateAsync(id, obj);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<CityDTO>> DeleteCityAsync(int id)
        {
            var response = await _cs.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response);
        }
    }
}
