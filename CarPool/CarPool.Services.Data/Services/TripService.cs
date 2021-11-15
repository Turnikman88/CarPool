using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class TripService : ITripService
    {
        private readonly CarPoolDBContext _db;
        private readonly ICheckExistenceService _check;
        private readonly ICityService _city;
        private readonly ICountryService _country;
        private readonly IApplicationUserService _ap;

        public TripService(CarPoolDBContext db, ICheckExistenceService check, ICityService city, ICountryService country, IApplicationUserService ap)
        {
            _db = db;
            _check = check;
            _city = city;
            _country = country;
            _ap = ap;
        }

        public async Task<IEnumerable<TripDTO>> GetAsync(int page)
        {
            return await this._db.Trips.Include(x => x.Driver)
                                 .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                 .Skip(page * GlobalConstants.PageSkip)
                                 .Take(10)
                                 .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<TripDTO> GetTripByIDAsync(int id)
        {
            _check.CheckId(id);

            var result = await this._db.Trips.Include(x => x.Driver)
                                 .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                 .FirstOrDefaultAsync(x=>x.Id == id);

            return result != null ? result.GetDTO() : new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
        }

        public async Task<TripDTO> PostAsync(TripDTO obj)
        {
            var post = obj.GetModel();

            await this._db.Trips.AddAsync(post);

            var result = post.GetDTO();
            return result;
        }

        public async Task<TripDTO> UpdateAsync(int id, TripDTO obj)
        {
            _check.CheckId(id);

            var toUpdate = await this._db.Trips.Include(x => x.StartAddress)
                                               .Include(x => x.DestinationAddress)
                                               .FirstOrDefaultAsync(x => x.Id == id);

            if (toUpdate is null)
                return new TripDTO() { ErrorMessage = GlobalConstants.TRIP_NOT_FOUND };

            toUpdate.AdditionalComment = obj.AdditionalComment;
            toUpdate.ArrivalTime = obj.ArrivalTime;
            toUpdate.ModifiedOn = System.DateTime.UtcNow;
            toUpdate.DepartureTime = obj.DepartureTime;
            toUpdate.DestinationAddressId = obj.DestinationAddressId;
            toUpdate.Distance = obj.Distance;
            toUpdate.DriverId = new Guid(obj.DriverId);
            toUpdate.FreeSeats = obj.FreeSeats;
            toUpdate.PassengersCount = obj.PassengersCount;
            toUpdate.Price = obj.Price;
            toUpdate.StartAddressId = obj.StartAddressId;

            await _db.SaveChangesAsync();

            return toUpdate.GetDTO();
            
        }

        public async Task<TripDTO> DeleteAsync(int id)
        {
            _check.CheckId(id);

            var trip = await this._db.Trips.FirstOrDefaultAsync(x => x.Id == id); //?? throw new AppException(GlobalConstants.TRIP_NOT_FOUND);

            if (trip != null)
                return new TripDTO() { ErrorMessage = GlobalConstants.TRIP_NOT_FOUND };

            this._db.Trips.Remove(trip);
            await _db.SaveChangesAsync();

            return trip.GetDTO();
        }

        public async Task<TripDTO> JoinTrip(int id, string userToJoinEmail)
        {
            _check.CheckId(id);

            var user = await this._ap.GetUserByEmailAsync(userToJoinEmail);

            if(user.ErrorMessage != null)
            {
                return new TripDTO { ErrorMessage = user.ErrorMessage };
            }

            if(user.IsBlocked == true)
            {
                return new TripDTO { ErrorMessage = GlobalConstants.TRIP_USER_BLOCKED_JOIN };
            }

            var trip = await this._db.Trips.FirstOrDefaultAsync(x => x.Id == id);
            if(trip == null)
            {
                return new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
            }

            if(trip.FreeSeats == 0)
            {
                return new TripDTO { ErrorMessage = GlobalConstants.TRIP_FULL };
            }

            trip.PassengersCount++;
            trip.FreeSeats--;

            await this._db.TripPassengers.AddAsync(new TripPassenger() { ApplicationUserId = user.Id, TripId = trip.Id });
            await this._db.SaveChangesAsync();

            return trip.GetDTO();
        }

        public async Task<TripDTO> LeaveTrip (int id, string userToLeaveEmail)
        {
            _check.CheckId(id);

            var user = await this._ap.GetUserByEmailAsync(userToLeaveEmail);

            if (user.ErrorMessage != null)
            {
                return new TripDTO { ErrorMessage = user.ErrorMessage };
            }
            
            var trip = await this._db.Trips.FirstOrDefaultAsync(x => x.Id == id);
            if (trip == null)
            {
                return new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
            }

            trip.PassengersCount--;
            trip.FreeSeats++;

            var tripEntity = await this._db.TripPassengers.FirstOrDefaultAsync(x => x.TripId == id && x.ApplicationUserId == user.Id);

            this._db.TripPassengers.Remove(tripEntity);
            await this._db.SaveChangesAsync();

            return trip.GetDTO();
        }
    }
}
