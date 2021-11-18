using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models.DatabaseModels
{
    public class UserVehicle : BaseModel<int>
    {
        public Guid ApplicationUserId { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public double FuelConsumptionPerHundredKilometers { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
