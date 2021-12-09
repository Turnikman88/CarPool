﻿using CarPool.Common;
using CarPool.Services.Mapping.Contracts;
using System.Collections.Generic;

namespace CarPool.Services.Mapping.DTOs
{
    public class ApplicationUserDisplayDTO : IErrorMessage
    {
        public ApplicationUserDisplayDTO()
        {
            Vehicle = GlobalConstants.NO_CAR_AVAILABLE;
            VehicleColor = GlobalConstants.NO_CAR_AVAILABLE;
        }
        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public double Rating { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Vehicle { get; set; }

        public string VehicleColor { get; set; }

        public bool IsBlocked { get; set; }

        public IEnumerable<string> Feedbacks { get; set; }

        public IEnumerable<string> Trips { get; set; }

        public string ErrorMessage { get; set; }
    }
}
