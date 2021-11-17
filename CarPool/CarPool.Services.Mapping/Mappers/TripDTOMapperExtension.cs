﻿using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                                       .Select(x=>x.ApplicationUser.FirstName + " " + x.ApplicationUser.LastName + " " + x.ApplicationUser.PhoneNumber)
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
    }
}
