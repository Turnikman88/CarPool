namespace CarPool.Data.Models.DatabaseModels
{
    using CarPool.Data.Common.Models;
    using System;

    public class Rating : BaseModel<int>
    {
        public Guid AddedByUserId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public int Value { get; set; }

        public string Feedback { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
