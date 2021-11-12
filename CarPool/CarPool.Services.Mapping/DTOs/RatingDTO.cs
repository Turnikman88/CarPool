using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }

        public Guid AddedByUserId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public int Value { get; set; }

        public string Feedback { get; set; }
    }
}
