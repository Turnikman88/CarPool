using System;

namespace CarPool.Data.Models.DatabaseModels
{
    public class TripPassenger
    {
        public int TripId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual Trip Trip { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
