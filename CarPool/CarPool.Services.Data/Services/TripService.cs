using CarPool.Common;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class TripService : ITripService
    {
        private readonly IApplicationUserService _ap;
        private readonly IAddressService _ads;
        private readonly IBingApiService _bing;
        private readonly ICheckExistenceService _check;
        private readonly ICityService _city;
        private readonly ICountryService _country;
        private readonly CarPoolDBContext _db;
        private readonly IFuelService _fuelService;

        public TripService(CarPoolDBContext db,
            ICheckExistenceService check,
            ICityService city,
            ICountryService country,
            IApplicationUserService ap,
            IBingApiService bing,
            IAddressService ads,
            IFuelService fuelService)
        {
            _ap = ap;
            _ads = ads;
            _bing = bing;
            _check = check;
            _city = city;
            _country = country;
            _db = db;
            _fuelService = fuelService;
        }

        public async Task<IEnumerable<TripDTO>> GetAsync(int page)
        {
            return await this._db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                        .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                        .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                        .Skip(page * GlobalConstants.PageSkip)
                                        .Take(10)
                                        .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<int> GetPageCountAsync()
        {
            var count = await this._db.Trips.CountAsync();
            var page = count / GlobalConstants.PageSkip;
            return page;
        }

        public async Task<int> GetPageCountPerUserAsync(string email)
        {
            var user = await _db.ApplicationUsers.Include(x => x.Address)
                                                  .Include(x => x.Ratings)
                                                  .Include(x => x.Trips).ThenInclude(x => x.DestinationAddress)
                                                  .Include(x => x.Vehicle)
                                                  .FirstOrDefaultAsync(x => x.Email == email);
            var count = user.Trips.Count();

            var page = count / GlobalConstants.PageSkip;
            return page;
        }

        public async Task<IEnumerable<TripDTO>> GetTripsByUserAsync(int page, string email) //TODO
        {
            var result = await _db.ApplicationUsers.Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                                   .Include(x => x.Ratings)
                                                   .Include(x => x.Trips).ThenInclude(x => x.DestinationAddress)
                                                   .Include(x => x.Trips).ThenInclude(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                                   .Include(x => x.Trips).ThenInclude(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                                   .Include(x => x.Trips).ThenInclude(x => x.Driver).ThenInclude(x => x.Vehicle)
                                                   .Include(x => x.Vehicle)
                                                   .Where(x => x.Email == email)
                                                   .Skip(page * GlobalConstants.PageSkip)
                                                   .Take(10)
                                                   .Select(x => x.Trips.Select(x => x.GetDTO())).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TripDTO> GetTripByIDAsync(int id)
        {
            _check.CheckId(id);

            var result = await this._db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                             .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                             .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                             .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                             .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                             .FirstOrDefaultAsync(x => x.Id == id);

            return result != null ? result.GetDTO() : new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
        }

        public async Task<TripDTO> PostAsync(TripDTO obj)
        {
            var cityOrigin = await _city.GetCityByNameAsync(obj.StartAddressCity);
            var cityDest = await _city.GetCityByNameAsync(obj.DestinationAddressCity);

            if (cityOrigin.ErrorMessage != null)
            {
                cityOrigin = await _city.PostAsync(new CityDTO { Name = obj.StartAddressCity, CountryName = obj.StartAddressCountry });
            }

            if (cityDest.ErrorMessage != null)
            {
                cityDest = await _city.PostAsync(new CityDTO { Name = obj.DestinationAddressCity, CountryName = obj.DestinationAddressCountry });
            }

            obj.StartAddressCountry = cityOrigin.CountryName;
            obj.DestinationAddressCountry = cityDest.CountryName;


            (int, int) travelData = (0, 0);
            if (obj.StartAddressStreet == null && obj.DestinationAddressStreet == null)
            {
                var addressIdOrigin = await _ads.GetAddressByCountryCityNameAsync(obj.StartAddressCity, obj.StartAddressCountry);
                var addressIdDestination = await _ads.GetAddressByCountryCityNameAsync(obj.DestinationAddressCity, obj.DestinationAddressCountry);
                travelData = await _bing.GetTripDataCoordinatesAsync($"{addressIdOrigin.Latitude},{addressIdOrigin.Longitude}", $"{addressIdDestination.Latitude},{addressIdDestination.Longitude} ");
            }
            else
            {
                var addressOrigin = await _ads.GetAddressByIdAsync(obj.StartAddressId);
                var addressDestination = await _ads.GetAddressByIdAsync(obj.DestinationAddressId);
                travelData = await _bing.GetTripDataCoordinatesAsync($"{addressOrigin.Latitude},{addressOrigin.Longitude}", $"{addressDestination.Latitude},{addressDestination.Longitude} ");
            }

            var vehicle = await _db.UserVehicles.FirstOrDefaultAsync(x => x.ApplicationUserId == Guid.Parse(obj.DriverId));

            obj.DurationInMinutes = travelData.Item2;
            obj.Distance = travelData.Item1;
            obj.Price = await _fuelService.Price(obj.Distance, vehicle.FuelConsumptionPerHundredKilometers) / (obj.FreeSeats + obj.PassengersCount);

            var post = obj.GetModel();

            await this._db.Trips.AddAsync(post);
            await _db.SaveChangesAsync();
            var tripPassenger = new TripPassenger() { ApplicationUserId = post.DriverId, TripId = post.Id, CreatedOn = DateTime.UtcNow };
            await this._db.TripPassengers.AddAsync(tripPassenger);
            await _db.SaveChangesAsync();

            return await GetTripByIDAsync(post.Id);
        }

        public async Task<TripDTO> UpdateAsync(int id, TripDTO obj)
        {
            _check.CheckId(id);

            var toUpdate = await this._db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                               .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                               .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                               .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                               .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                               .FirstOrDefaultAsync(x => x.Id == id);

            if (toUpdate is null)
                return new TripDTO() { ErrorMessage = GlobalConstants.TRIP_NOT_FOUND };

            var travelData = await _bing.GetTripDataCityCountryAsync($"{obj.StartAddressCity} {obj.StartAddressCountry}", $"{obj.DestinationAddressCity} {obj.DestinationAddressCountry}");

            var price = await _fuelService.Price(obj.Distance, toUpdate.Driver.Vehicle.FuelConsumptionPerHundredKilometers) / (obj.FreeSeats + obj.PassengersCount);

            toUpdate.AdditionalComment = obj.AdditionalComment;
            toUpdate.DurationInMinutes = travelData.Item2;
            toUpdate.ModifiedOn = System.DateTime.UtcNow;
            toUpdate.DepartureTime = obj.DepartureTime;
            toUpdate.DestinationAddressId = obj.DestinationAddressId;
            toUpdate.Distance = travelData.Item1;
            toUpdate.DriverId = new Guid(obj.DriverId);
            toUpdate.FreeSeats = obj.FreeSeats;
            toUpdate.PassengersCount = obj.PassengersCount;
            toUpdate.Price = price;
            toUpdate.StartAddressId = obj.StartAddressId;

            await _db.SaveChangesAsync();

            return toUpdate.GetDTO();

        }

        public async Task<TripDTO> DeleteAsync(int id)
        {
            _check.CheckId(id);

            var trip = await this._db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                           .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                           .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == id);

            if (trip != null)
                return new TripDTO() { ErrorMessage = GlobalConstants.TRIP_NOT_FOUND };

            this._db.Trips.Remove(trip);
            await _db.SaveChangesAsync();

            return trip.GetDTO();
        }

        public async Task<TripDTO> JoinTripAsync(int id, string userToJoinEmail)
        {
            _check.CheckId(id);

            var user = await this._ap.GetUserByEmailAsync(userToJoinEmail);

            if (user.ErrorMessage != null)
            {
                return new TripDTO { ErrorMessage = user.ErrorMessage };
            }

            if (user.IsBlocked == true)
            {
                return new TripDTO { ErrorMessage = GlobalConstants.TRIP_USER_BLOCKED_JOIN };
            }

            var trip = await this._db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                           .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                           .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == id);
            if (trip == null)
            {
                return new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
            }

            if (trip.FreeSeats == 0)
            {
                return new TripDTO { ErrorMessage = GlobalConstants.TRIP_FULL };
            }

            trip.PassengersCount++;
            trip.FreeSeats--;

            await this._db.TripPassengers.AddAsync(new TripPassenger() { ApplicationUserId = user.Id, TripId = trip.Id });
            await this._db.SaveChangesAsync();

            return trip.GetDTO();
        }

        public async Task<TripDTO> LeaveTripAsync(int id, string userToLeaveEmail)
        {
            _check.CheckId(id);

            var user = await this._ap.GetUserByEmailAsync(userToLeaveEmail);

            if (user.ErrorMessage != null)
            {
                return new TripDTO { ErrorMessage = user.ErrorMessage };
            }

            var trip = await this._db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                           .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                           .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == id);
            if (trip == null)
            {
                return new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
            }

            trip.PassengersCount--;
            trip.FreeSeats++;

            var tripEntity = await this._db.TripPassengers.FirstOrDefaultAsync(x => x.TripId == id && x.ApplicationUserId == user.Id);

            var removed = this._db.TripPassengers.Remove(tripEntity);
            await this._db.SaveChangesAsync();

            return trip.GetDTO();
        }

        public async Task<int> TripsCountAsync()
        {
            return await _db.Trips.CountAsync();
        }
    }
}
