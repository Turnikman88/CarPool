using CarPool.API.Infrastructure.Attributes;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _ads;

        public AddressController(IAddressService ads)
        {
            _ads = ads;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAddressAsync(int page)
        {
            return Ok(await _ads.GetAsync(page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> GetAddressByIdAsync(int id)
        {
            var response = await _ads.GetAddressByIdAsync(id);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> CreateAddressAsync(AddressDTO obj)
        {
            var response = await _ads.PostAsync(obj);

            if (response.ErrorMessage is null)
            {
                return Created("Get", response);
            }
            return NotFound(response.ErrorMessage);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> UpdateAddressAsync(int id, AddressDTO obj)
        {
            var response = await _ads.UpdateAsync(id, obj);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(response.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> DeleteAddressAsync(int id)
        {
            var response = await _ads.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(response.ErrorMessage);
        }
    }
}
