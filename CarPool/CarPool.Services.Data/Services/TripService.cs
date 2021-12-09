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
        private readonly CarPoolDBContext _db;
        private readonly IFuelService _fus;
        private readonly IInboxService _inbs;

        public TripService(CarPoolDBContext db,
            ICheckExistenceService check,
            IApplicationUserService ap,
            IBingApiService bing,
            IAddressService ads,
            IFuelService fus,
            IInboxService inbs)
        {
            _ap = ap;
            _ads = ads;
            _bing = bing;
            _check = check;
            _db = db;
            _fus = fus;
            _inbs = inbs;
        }

        public async Task<IEnumerable<TripDTO>> GetAsync(int page)
        {
            var totla = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                        .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                        .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                        .OrderBy(x => x.DepartureTime)
                                        .Skip(page * GlobalConstants.PageSkip)
                                        .Take(10).Select(x => x.GetDTO()).ToListAsync();
            return totla;
        }



        public async Task<IEnumerable<TripDTO>> GetAllUpcomingTripsAsync(int page)
        {
            return await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                        .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                        .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                        .Where(x => x.DepartureTime.Date >= DateTime.Today.Date)
                                        .OrderBy(x => x.DepartureTime)
                                        .Skip(page * GlobalConstants.PageSkip)
                                        .Take(10)
                                        .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<int> GetPageCountAsync()
        {
            var count = await _db.Trips.Where(x => x.DepartureTime.Date >= DateTime.Today.Date).CountAsync();
            var page = count / GlobalConstants.PageSkip;
            return page;
        }

        public async Task<int> GetPageCountPerUserAsync(string userIdentificator)
        {
            var user = await _db.ApplicationUsers.Include(x => x.Address)
                                                  .Include(x => x.Ratings)
                                                  .Include(x => x.Trips).ThenInclude(x => x.DestinationAddress)
                                                  .Include(x => x.Vehicle)
                                                  .FirstOrDefaultAsync(x => x.Email == userIdentificator || x.Id.ToString() == userIdentificator);
            var count = user.Trips.Where(x => x.DepartureTime.Date >= DateTime.Today.Date).Count();

            var page = count / GlobalConstants.PageSkip;
            return page;
        }

        public async Task<IEnumerable<TripDTO>> GetPastByUserTrips(int page, string userIdentificator)
        {
            return await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                  .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                  .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                  .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                  .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                  .Where(x => x.DepartureTime.Date < DateTime.Today.Date && (x.Passengers.Any(x => x.ApplicationUser.Email == userIdentificator || x.ApplicationUser.Id.ToString() == userIdentificator) || x.Driver.Email == userIdentificator))
                                  .OrderBy(x => x.DepartureTime)
                                  .Skip(page * GlobalConstants.PageSkip)
                                  .Take(10)
                                  .Select(x => x.GetDTO()).ToListAsync();
        }
        public async Task<IEnumerable<TripDTO>> GetUpcomingTripsByUserAsync(int page, string userIdentificator)
        {
            return await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                  .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                  .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                  .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                  .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                  .Where(x => x.DepartureTime.Date >= DateTime.Today.Date && x.Passengers.Any(x => x.ApplicationUser.Email == userIdentificator /*|| x.ApplicationUser.Id == Guid.Parse(userIdentificator)*/))
                                  .OrderBy(x => x.DepartureTime)
                                  .Skip(page * GlobalConstants.PageSkip)
                                  .Take(10)
                                  .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<TripDriverDTO>> GetUpcomingTripsByUserAsDriverAsync(int page, string email)
        {
            var query = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                       .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                       .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                       .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                       .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser).ThenInclude(x => x.ApplicationRole)
                                       .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser).ThenInclude(x => x.ProfilePicture)
                                       .Where(x => x.DepartureTime.Date >= DateTime.Today.Date && x.Driver.Email == email)
                                       .OrderBy(x => x.DepartureTime)
                                       .Skip(page * GlobalConstants.PageSkip)
                                       .Take(10)
                                       .ToListAsync();

            List<TripDriverDTO> trips = new List<TripDriverDTO>();

            foreach (var trp in query)
            {
                var model = trp.GetTripDriverDTO();
                var colll = trp.Passengers.Select(x => x.ApplicationUser.GetDTOWithPicture()).ToList();
                model.Passengers = colll;
                trips.Add(model);
            }

            return trips;
        }

        public async Task<TripDTO> GetTripByIDAsync(int id)
        {
            _check.CheckId(id);

            var result = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                        .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                        .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                        .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            return result != null ? result.GetDTO() : new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
        }

        public async Task<TripDTO> PostAsync(TripDTO obj)
        {
            var originAddressID = await _ads.AddressToId(
                new AddressDTO
                {
                    CityName = obj.StartAddressCity,
                    CountryName = obj.StartAddressCountry,
                    StreetName = obj.StartAddressStreet
                });

            var destinationAddressID = await _ads.AddressToId(
                new AddressDTO
                {
                    CityName = obj.DestinationAddressCity,
                    CountryName = obj.DestinationAddressCountry,
                    StreetName = obj.DestinationAddressStreet
                });

            obj.StartAddressId = originAddressID;
            obj.DestinationAddressId = destinationAddressID;

            var addressOriginData = await _ads.GetAddressByIdAsync(obj.StartAddressId);
            var addressDestinationData = await _ads.GetAddressByIdAsync(obj.DestinationAddressId);
            var travelData = await _bing.GetTripDataByCoordinatesAsync($"{addressOriginData.Latitude},{addressOriginData.Longitude}", $"{addressDestinationData.Latitude},{addressDestinationData.Longitude}");

            var vehicle = await _db.UserVehicles.FirstOrDefaultAsync(x => x.ApplicationUserId == Guid.Parse(obj.DriverId));

            obj.DurationInMinutes = travelData.Item2;
            obj.Distance = travelData.Item1;
            obj.Price = await _fus.Price(obj.Distance, vehicle.FuelConsumptionPerHundredKilometers) / (obj.FreeSeats + obj.PassengersCount);

            var post = obj.GetModel();

            await _db.Trips.AddAsync(post);
            await _db.SaveChangesAsync();

            var tripPassenger = new TripPassenger()
            {
                ApplicationUserId = post.DriverId,
                TripId = post.Id,
            };

            await _db.TripPassengers.AddAsync(tripPassenger);
            await _db.SaveChangesAsync();

            return await GetTripByIDAsync(post.Id);
        }

        public async Task<TripDTO> UpdateAsync(int id, TripDTO obj)
        {
            _check.CheckId(id);

            var toUpdate = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                           .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                           .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (toUpdate is null)
            {
                return new TripDTO() { ErrorMessage = GlobalConstants.TRIP_NOT_FOUND };
            }

            var originAddressID = await _ads.AddressToId(
              new AddressDTO
              {
                  CityName = obj.StartAddressCity,
                  CountryName = obj.StartAddressCountry,
                  StreetName = obj.StartAddressStreet
              });

            var destinationAddressID = await _ads.AddressToId(
                new AddressDTO
                {
                    CityName = obj.DestinationAddressCity,
                    CountryName = obj.DestinationAddressCountry,
                    StreetName = obj.DestinationAddressStreet
                });

            var addressOriginData = await _ads.GetAddressByIdAsync(originAddressID);
            var addressDestinationData = await _ads.GetAddressByIdAsync(destinationAddressID);

            var travelData = await _bing.GetTripDataByCityCountryAsync($"{obj.StartAddressCity} {obj.StartAddressCountry}", $"{obj.DestinationAddressCity} {obj.DestinationAddressCountry}");

            var price = await _fus.Price(obj.Distance, toUpdate.Driver.Vehicle.FuelConsumptionPerHundredKilometers) / (obj.FreeSeats + obj.PassengersCount);

            toUpdate.AdditionalComment = obj.AdditionalComment;
            toUpdate.DurationInMinutes = travelData.Item2;
            toUpdate.ModifiedOn = System.DateTime.UtcNow;
            toUpdate.StartAddressId = originAddressID;
            toUpdate.DestinationAddressId = destinationAddressID;
            toUpdate.DepartureTime = obj.DepartureTime;
            toUpdate.Distance = travelData.Item1;
            toUpdate.DriverId = new Guid(obj.DriverId);
            toUpdate.FreeSeats = obj.FreeSeats;
            toUpdate.PassengersCount = obj.PassengersCount;
            toUpdate.Price = price;

            await _db.SaveChangesAsync();

            return toUpdate.GetDTO();

        }

        public async Task<TripDTO> DeleteAsync(int id)
        {
            _check.CheckId(id);

            var trip = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                           .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                           .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == id);

            if (trip == null)
            {
                return new TripDTO() { ErrorMessage = GlobalConstants.TRIP_NOT_FOUND };
            }
            var passengers = await _db.TripPassengers.Where(x => x.TripId == trip.Id).ToListAsync();

            foreach (var person in passengers)
            {
                _db.TripPassengers.Remove(person);
            }

            _db.Trips.Remove(trip);
            await _db.SaveChangesAsync();

            return trip.GetDTO();
        }

        public async Task<TripDTO> JoinTripAsync(int id, string userToJoinEmail)
        {
            _check.CheckId(id);

            var user = await _ap.GetUserByEmailOrIdAsync(userToJoinEmail);

            if (user.ErrorMessage != null)
            {
                return new TripDTO { ErrorMessage = user.ErrorMessage };
            }

            if (user.IsBlocked == true)
            {
                return new TripDTO { ErrorMessage = GlobalConstants.TRIP_USER_BLOCKED_JOIN };
            }

            var trip = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
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

            await _db.TripPassengers.AddAsync(new TripPassenger() { ApplicationUserId = user.Id, TripId = trip.Id, CreatedOn = DateTime.UtcNow });
            await _inbs.SendMessageAsync(user.Id.ToString(), trip.DriverId.ToString(), string.Format(GlobalConstants.JOIN_USER_TRIP_MESSAGE, trip.DepartureTime.ToString("d/MM/yy"), trip.StartAddress.City.Name, trip.DestinationAddress.City.Name));
            await _db.SaveChangesAsync();

            return trip.GetDTO();
        }

        public async Task<TripDTO> LeaveTripAsync(int id, string userToLeaveEmail)
        {
            _check.CheckId(id);

            var user = await _ap.GetUserByEmailOrIdAsync(userToLeaveEmail);

            if (user.ErrorMessage != null)
            {
                return new TripDTO { ErrorMessage = user.ErrorMessage };
            }

            var trip = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                           .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                           .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                           .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == id);
            if (trip == null)
            {
                return new TripDTO() { ErrorMessage = (GlobalConstants.TRIP_NOT_FOUND) };
            }

            var tripEntity = await _db.TripPassengers.FirstOrDefaultAsync(x => x.TripId == id && x.ApplicationUserId == user.Id);
            _db.TripPassengers.Remove(tripEntity);

            if (trip.DriverId == user.Id)
            {
                return await DeleteAsync(trip.Id);
            }

            trip.PassengersCount--;
            trip.FreeSeats++;
            await _db.SaveChangesAsync();
            return trip.GetDTO();

        }

        public async Task<bool> KickUserAsync(string userId, int tripId, string requestEmail)
        {
            var driver = await _ap.GetUserByEmailOrIdAsync(requestEmail);
            var trip = await _db.Trips.Include(x => x.Driver).ThenInclude(x => x.Vehicle)
                                             .Include(x => x.StartAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                             .Include(x => x.DestinationAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                                             .Include(x => x.Passengers).ThenInclude(x => x.Trip)
                                             .Include(x => x.Passengers).ThenInclude(x => x.ApplicationUser)
                                             .FirstOrDefaultAsync(x => x.Id == tripId);

            if (trip is null)
            {
                return false;
            }

            if (driver.Id != trip.DriverId)
            {
                return false;
            }

            var tripPassengerID = await _db.TripPassengers.FirstOrDefaultAsync(x => x.TripId == tripId && x.ApplicationUserId.ToString() == userId);
            _db.Remove(tripPassengerID);
            trip.PassengersCount--;
            trip.FreeSeats++;
            await _inbs.SendMessageAsync(driver.Email, userId, string.Format(GlobalConstants.KICK_USER_MESSAGE, trip.DepartureTime, trip.StartAddress.City.Name, trip.DestinationAddress.City.Name));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<int> TripsCountAsync()
        {
            return await _db.Trips.CountAsync();
        }

    }
}
