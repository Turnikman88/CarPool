using System;

namespace CarPool.Services.Mapping.DTOs
{
    public class UserVehicleDTO
    {
        public int Id { get; set; }

        public Guid ApplicationUserId { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public double FuelConsumptionPerHundredKilometers { get; set; }

        public string ErrorMessage { get; set; }
    }
}
