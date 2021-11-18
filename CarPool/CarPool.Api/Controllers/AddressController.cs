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
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAddressAsync(int page)
        {
            return this.Ok(await _ads.GetAsync(page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<AddressDTO>> GetAddressByIdAsync(int id)
        {
            var response = await _ads.GetAddressByIdAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<AddressDTO>> CreateAddressAsync(AddressDTO obj)
        {
            var response = await _ads.PostAsync(obj);

            if (response.ErrorMessage is null)
            {
                return this.Created("Get", response);
            }
            return this.NotFound(response.ErrorMessage);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<AddressDTO>> UpdateAddressAsync(int id, AddressDTO obj)
        {
            var response = await _ads.UpdateAsync(id, obj);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<AddressDTO>> DeleteAddressAsync(int id)
        {
            var response = await _ads.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response.ErrorMessage);
        }
    }
}
