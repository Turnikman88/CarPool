namespace CarPool.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using CarPool.Data.Common.Models;

    public class Rating : BaseModel<int>
    {
        public Guid AddedByUserId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public int Value { get; set; }

        public string Feedback { get; set; }
    }
}
