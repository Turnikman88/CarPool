namespace CarPool.Data.Models.DatabaseModels
{
    using CarPool.Data.Common.Models;
    using System;

    public class ProfilePicture : BaseModel<int>
    {      
        public string ImageTitle { get; set; }

        public byte[] ImageData { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
