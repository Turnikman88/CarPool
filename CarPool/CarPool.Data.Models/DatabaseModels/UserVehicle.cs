using CarPool.Data.Common.Models;
using System;

namespace CarPool.Data.Models.DatabaseModels
{
    public class UserVehicle : BaseDeletableModel<int>
    {
        public Guid ApplicationUserId { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public double FuelConsumptionPerHundredKilometers { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
