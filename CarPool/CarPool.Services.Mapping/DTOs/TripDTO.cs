using CarPool.Common;
using CarPool.Services.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class TripDTO : IErrorMessage
    {
        public string DriverId { get; set; }

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

        public DateTime ArrivalTime { get; set; }

        public double Distance { get; set; }

        public decimal Price { get; set; }

        public int PassengersCount { get; set; }

        public int FreeSeats { get; set; }

        public List<string> PassengersNameID { get; set; } = new List<string>();

        public string AdditionalComment { get; set; } = GlobalConstants.NO_COMMENT;

        public string ErrorMessage { get; set; }
    }
}
