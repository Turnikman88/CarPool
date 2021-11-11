
using CarPool.Data.Common.Models;
using System.Collections.Generic;

namespace CarPool.Data.Models.DatabaseModels
{
    public class Address : BaseDeletableModel<int>
    {
        public Address()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
            this.TripsDestinationAddress = new HashSet<Trip>();
            this.TripsStartAddress = new HashSet<Trip>();
        }

        public int CityId { get; set; }

        public string StreetName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Trip> TripsDestinationAddress { get; set; }

        public virtual ICollection<Trip> TripsStartAddress { get; set; }
    }
}
