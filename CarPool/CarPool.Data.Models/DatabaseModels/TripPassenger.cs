﻿using System;

namespace CarPool.Data.Models.DatabaseModels
{
    public class TripPassenger
    {
        public int TripId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public Trip Trip { get; set; }
        public Trip TripTest { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

    }
}
