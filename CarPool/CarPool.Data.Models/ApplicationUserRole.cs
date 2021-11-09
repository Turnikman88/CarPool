namespace CarPool.Data.Models
{
    using System;

    using CarPool.Data.Common.Models;

    public class ApplicationUserRole : IDeletableEntity
    {
        public int ApplicationRoleId { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }

        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
