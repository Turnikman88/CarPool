using CarPool.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class ApplicationUserDisplayDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public double Rating { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Vehicle { get; set; } = GlobalConstants.NO_CAR_AVAILABLE;

        public string VehicleColor { get; set; } = GlobalConstants.NO_CAR_AVAILABLE;

        public bool IsBlocked { get; set; }

        public IEnumerable<string> Feedbacks { get; set; }

        public IEnumerable<string> Trips { get; set; }
    }
}
