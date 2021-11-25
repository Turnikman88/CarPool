using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class TopUserDTO
    {
        public string ProfilePicture { get; set; }

        public string Username { get; set; }

        public double Rating { get; set; }

        public int CompletedTripsCount { get; set; }
    }
}
