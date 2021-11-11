namespace CarPool.Data.Models.DatabaseModels
{
    using CarPool.Common;
    using CarPool.Data.Common.Models;
    using System;

    public class ProfilePicture : BaseModel<int>
    {
        public string ImageTitle { get; set; } = GlobalConstants.NO_TITLE;

        public byte[] ImageData { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
