using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;

namespace CarPool.Web.ViewModels.DTOs
{
    public class TripViewModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public DateTime Date { get; set; }

        public int FreeSeats { get; set; }

        public string AdditionalComment { get; set; }

        public IEnumerable<TripDTO> Trips { get; set; }
    }
}
