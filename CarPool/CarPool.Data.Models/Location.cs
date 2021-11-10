namespace CarPool.Data.Models
{
    using System;

    using CarPool.Data.Common.Models;

    public class Location : BaseDeletableModel<Guid>
    {
        public Location()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid ApplicationUserId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
