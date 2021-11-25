using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Web.ViewModels.DTOs
{
    public class TripViewModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public int MaxPages { get; set; }

        public int CurrentPage { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int FreeSeats { get; set; }

        public string AdditionalComment { get; set; }

        public IEnumerable<TripDTO> Trips { get; set; }
    }
}
