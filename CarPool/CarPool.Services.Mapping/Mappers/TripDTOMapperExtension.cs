using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.Mappers
{
    public static class TripDTOMapperExtension
    {
        public static TripDTO GetDTO(this Trip trip)
        {
            return new TripDTO
            {
                DriverId = trip.DriverId.ToString(),
                DriverName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                StartAddressId = trip.StartAddressId,
                StartAddressCountry = trip.StartAddress.City.Country.Name,
                StartAddressCity = trip.StartAddress.City.Name,
                StartAddressStreet = trip.StartAddress.StreetName,
                DestinationAddressId = trip.DestinationAddressId,
                DestinationAddressCountry = trip.DestinationAddress.City.Country.Name,
                DestinationAddressCity = trip.DestinationAddress.City.Name,
                DestinationAddressStreet = trip.DestinationAddress.StreetName,
                DepartureTime = trip.DepartureTime,
                ArrivalTime = trip.ArrivalTime,
                Distance = trip.Distance,
                FreeSeats = trip.FreeSeats,
                PassengersCount = trip.PassengersCount,
                Price = trip.Price,
                AdditionalComment = trip.AdditionalComment
            };
        }

        public static Trip GetModel(this TripDTO tripDTO)
        {
            return new Trip
            {
            };
        }
    }
}
