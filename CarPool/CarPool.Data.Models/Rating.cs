namespace CarPool.Data.Models
{
    using System;

    using CarPool.Data.Common.Models;

    public class Rating : BaseModel<Guid>
    {
        public Rating()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid ApplicationUserId { get; set; }

        public int Value { get; set; }

        public string Feedback { get; set; }
    }
}
