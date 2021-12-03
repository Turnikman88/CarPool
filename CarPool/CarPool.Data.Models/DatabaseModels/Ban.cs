using CarPool.Data.Common.Models;
using System;

namespace CarPool.Data.Models.DatabaseModels
{
    public class Ban : BaseModel<int>
    {
        public DateTime? BlockedOn { get; set; }

        public DateTime? BlockedDue { get; set; }

        public Guid ApplicationUserId { get; set; }

        public string Reason { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
