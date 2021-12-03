using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Linq;

namespace CarPool.Services.Mapping.Mappers
{
    public static class TripDTOMapperExtension
    {
        public static TripDTO GetDTO(this Trip trip)
        {
            return new TripDTO
            {
                Id = trip.Id,
                DriverId = trip.DriverId.ToString(),
                DriverEmail = trip.Driver.Email,
                DriverName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                DriverPhonenumber = trip.Driver.PhoneNumber,
                DriverVehicle = trip.Driver.Vehicle.Model,
                DriverVehicleColor = trip.Driver.Vehicle.Color,
                StartAddressId = trip.StartAddressId,
                StartAddressCountry = trip.StartAddress.City.Country.Name,
                StartAddressCity = trip.StartAddress.City.Name,
                StartAddressStreet = trip.StartAddress.StreetName,
                DestinationAddressId = trip.DestinationAddressId,
                DestinationAddressCountry = trip.DestinationAddress.City.Country.Name,
                DestinationAddressCity = trip.DestinationAddress.City.Name,
                DestinationAddressStreet = trip.DestinationAddress.StreetName,
                DepartureTime = trip.DepartureTime,
                DurationInMinutes = trip.DurationInMinutes,
                Distance = trip.Distance,
                FreeSeats = trip.FreeSeats,
                PassengersCount = trip.PassengersCount,
                Price = trip.Price,
                AdditionalComment = trip.AdditionalComment,
                PassengersNameAndPhone = trip.Passengers
                                       .Where(p => p.TripId == trip.Id)
                                       .Select(x => x.ApplicationUser.FirstName + " " + x.ApplicationUser.LastName + " " + x.ApplicationUser.PhoneNumber)
                                       .ToList()
            };
        }

        public static Trip GetModel(this TripDTO tripDTO)
        {
            return new Trip
            {
                DriverId = Guid.Parse(tripDTO.DriverId),
                StartAddressId = tripDTO.StartAddressId,
                DestinationAddressId = tripDTO.DestinationAddressId,
                DepartureTime = tripDTO.DepartureTime,
                DurationInMinutes = tripDTO.DurationInMinutes,
                Distance = tripDTO.Distance,
                FreeSeats = tripDTO.FreeSeats,
                PassengersCount = tripDTO.PassengersCount,
                Price = tripDTO.Price,
                AdditionalComment = tripDTO.AdditionalComment
            };
        }

        public static TripDriverDTO GetTripDriverDTO(this Trip trip)
        {
            return new TripDriverDTO
            {
                Id = trip.Id,
                DepartureTime = trip.DepartureTime,
                AdditionalComment = trip.AdditionalComment,
                Distance = trip.Distance,
                DestinationAddressCity = trip.DestinationAddress.City.Name,
                DestinationAddressCountry = trip.DestinationAddress.City.Country.Name,
                StartAddressCity = trip.StartAddress.City.Name,
                StartAddressCountry = trip.StartAddress.City.Country.Name,
                FreeSeats = trip.FreeSeats,
                PassengersCount = trip.PassengersCount,
                DurationInMinutes = trip.DurationInMinutes
            };
        }
    }
}
