namespace CarPool.Data.Models.DatabaseModels
{
    using CarPool.Common;
    using CarPool.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProfilePicture : BaseDeletableModel<int>
    {
        public string ImageLink { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
