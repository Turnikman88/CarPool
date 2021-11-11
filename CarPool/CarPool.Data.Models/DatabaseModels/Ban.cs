using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models.DatabaseModels
{
    public class Ban : BaseModel<int>
    {
        public bool IsPermanentBlock { get; set; }

        public DateTime? BlockedOn { get; set; }

        public DateTime? BlockedDue { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
