﻿using CarPool.Common;
using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace CarPool.Data.Models.DatabaseModels
{
    public class Trip : BaseModel<int>
    {
        public Trip()
        {
            Passengers = new HashSet<TripPassenger>();
        }

        public Guid DriverId { get; set; }

        public int StartAddressId { get; set; }

        public int DestinationAddressId { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public double Distance { get; set; }

        public decimal Price { get; set; }

        public int PassengersCount { get; set; }

        public int FreeSeats { get; set; }

        public virtual ICollection<TripPassenger> Passengers { get; set; }

        public string AdditionalComment { get; set; } = GlobalConstants.NO_COMMENT;

        public virtual ApplicationUser Driver { get; set; }

        public virtual Address DestinationAddress { get; set; }

        public virtual Address StartAddress { get; set; }
    }
}
