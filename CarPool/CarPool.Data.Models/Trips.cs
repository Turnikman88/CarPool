using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models
{
    public class Trips : BaseDeletableModel<int>
    {
        public Guid DriverId { get; set; }

        public Guid ApplicationuserId { get; set; }

        public int StartAddressId { get; set; }

        public int DestinationAddressId { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public int PassengersCount { get; set; }

        public int FreeSeats { get; set; }

        public string AdditionalComment { get; set; }

        public virtual ApplicationUser Driver { get; set; }

        public virtual Address DestinationAddress { get; set; }

        public virtual Address StartAddress { get; set; }
    }
}
