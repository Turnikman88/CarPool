using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models
{
    public class TripPassenger : BaseDeletableModel<int>
    {
        public Guid TripId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Trips Trip { get; set; }
    }
}
