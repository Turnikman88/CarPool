namespace CarPool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using CarPool.Data.Common.Models;

    public class ProfilePicture : BaseModel<Guid>
    {
        public ProfilePicture()
        {
            this.Id = Guid.NewGuid();
        }
        
        public string ImageTitle { get; set; }

        public byte[] ImageData { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
