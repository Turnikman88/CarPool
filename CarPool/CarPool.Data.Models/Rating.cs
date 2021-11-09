namespace CarPool.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using CarPool.Data.Common.Models;

    public class Rating : BaseModel<Guid>
    {
        public Rating()
        {
            this.Id = Guid.NewGuid();
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public Guid ApplicationUserId { get; set; }

        public int Value { get; set; }

        public string Feedback { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
