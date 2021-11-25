using CarPool.API.Infrastructure.Attributes;
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
    public class TripController : ControllerBase
    {
        private readonly ITripService _ts;

        public TripController(ITripService ts)
        {
            _ts = ts;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TripDTO>>> GetTripsAsync(int page)
        {
            return this.Ok(await _ts.GetAsync(page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<TripDTO>> GetTripByIdAsync(int id)
        {
            var response = await _ts.GetTripByIDAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response.ErrorMessage);
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<TripDTO>> CreateTripAsync(TripDTO obj)
        {
            var response = await _ts.PostAsync(obj);

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
        [Authorize]
        public async Task<ActionResult<TripDTO>> UpdateTripAsync(int id, TripDTO obj)
        {
            var response = await _ts.UpdateAsync(id, obj);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response.ErrorMessage);
        }

        [HttpPut("{email}/join/{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<TripDTO>> JoinTripAsync(int id, string email)
        {
            var response = await _ts.JoinTripAsync(id, email);

            if (response.ErrorMessage is null)
            {
                return this.Accepted(response);
            }

            return this.BadRequest(response.ErrorMessage);
        }

        [HttpPut("{email}/leave/{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<TripDTO>> LeaveTripAsync(int id, string email)
        {
            var response = await _ts.LeaveTripAsync(id, email);

            if (response.ErrorMessage is null)
            {
                return this.Accepted(response);
            }

            return this.BadRequest(response.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<TripDTO>> DeleteTripAsync(int id)
        {
            var response = await _ts.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return this.Ok(response);
            }

            return this.NotFound(response.ErrorMessage);
        }

    }
}
