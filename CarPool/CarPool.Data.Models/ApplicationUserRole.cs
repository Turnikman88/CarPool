namespace CarPool.Data.Models
{
    using System;

    using CarPool.Data.Common.Models;

    public class ApplicationUserRole : IDeletableEntity
    {
        public int AppRoleId { get; set; }

        public virtual ApplicationRole AppRole { get; set; }

        public int AppUserId { get; set; }

        public virtual ApplicationUser AppUser { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
