
using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models
{
    public class Address : BaseDeletableModel<int>
    {
        public Address()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
            this.TripsDestinationAddress = new HashSet<Trips>();
            this.TripsStartAddress = new HashSet<Trips>();
        }

        public int CityId { get; set; }

        public string StreetName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Trips> TripsDestinationAddress { get; set; }
        
        public virtual ICollection<Trips> TripsStartAddress { get; set; }
    }
}
