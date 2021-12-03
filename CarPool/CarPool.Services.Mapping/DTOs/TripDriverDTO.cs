﻿using CarPool.Common;
using System;
using System.Collections.Generic;

namespace CarPool.Services.Mapping.DTOs
{
    public class TripDriverDTO
    {
        public int Id { get; set; }

        public string DriverId { get; set; }

        public string DriverEmail { get; set; }

        public string DriverName { get; set; }

        public string DriverVehicle { get; set; }

        public string DriverVehicleColor { get; set; }

        public string DriverPhonenumber { get; set; }

        public int StartAddressId { get; set; }

        public string StartAddressCity { get; set; }

        public string StartAddressCountry { get; set; }

        public string StartAddressStreet { get; set; }

        public int DestinationAddressId { get; set; }

        public string DestinationAddressCountry { get; set; }

        public string DestinationAddressCity { get; set; }

        public string DestinationAddressStreet { get; set; }

        public DateTime DepartureTime { get; set; }

        public int DurationInMinutes { get; set; }

        public int Distance { get; set; }

        public decimal Price { get; set; }

        public int PassengersCount { get; set; }

        public int FreeSeats { get; set; }

        public ICollection<ApplicationUserDTO> Passengers { get; set; } = new List<ApplicationUserDTO>();

        public string AdditionalComment { get; set; } = GlobalConstants.NO_COMMENT;

        public string ErrorMessage { get; set; }
    }
}
