using CarPool.API.Infrastructure.Attributes;
using CarPool.Common;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountriesAsync(int page)
        {
            return Ok(await _cs.GetAsync(page));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<CountryDTO>> GetCountryByIdAsync(int id)
        {
            var response = await _cs.GetCountryByIdAsync(id);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }
            return NotFound(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<CountryDTO>> GetCountryByNameAsync(string name)
        {
            var response = await _cs.GetCountryByNameAsync(name);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }
            return NotFound(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpGet("partname/{part}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountryByNamePartAsync(int page, string part)
        {
            return Ok(await _cs.GetCountriesByPartNameAsync(page, part));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<CountryDTO>> CreateCountryAsync(CountryDTO obj)
        {
            var response = await _cs.PostAsync(obj);

            if (response.ErrorMessage is null)
            {
                return Created("Get", response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<CountryDTO>> UpdateCountryAsync(int id, CountryDTO obj)
        {
            var response = await _cs.UpdateAsync(id, obj);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<CountryDTO>> DeleteCountryAsync(int id)
        {
            var response = await _cs.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }
    }
}
