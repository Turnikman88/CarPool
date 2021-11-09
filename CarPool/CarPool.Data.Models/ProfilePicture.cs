namespace CarPool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using CarPool.Data.Common.Models;

    public class ProfilePicture : BaseModel<Guid>
    {
        // The contents of the image is in the file system
        public ProfilePicture()
        {
            this.Id = Guid.NewGuid();
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public Guid AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public string Extension { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
