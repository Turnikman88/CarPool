using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Web.ViewModels.DTOs
{
    public class RatingViewModel
    {
        [Range(1, 5)]
        public int Value { get; set; }

        public string UserID { get; set; }

        public string Comment { get; set; }

        public int TripId { get; set; }
    }
}
