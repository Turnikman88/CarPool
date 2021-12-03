using CarPool.Data.Common.Models;
using System;

namespace CarPool.Data.Models.DatabaseModels
{
    public class Inbox : BaseModel<int>
    {
        public Guid FromUserId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public string Message { get; set; }

        public bool Seen { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
