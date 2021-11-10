using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models
{
    public class TripPassenger
    {
        public int TripId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Trips Trip { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

    }
}
