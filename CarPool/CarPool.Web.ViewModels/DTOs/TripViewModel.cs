using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Web.ViewModels.DTOs
{
    public class TripViewModel
    {

        public string OriginCountry { get; set; }

        public string OriginCity { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationCity { get; set; }

        public int MaxPages { get; set; }

        public int CurrentPage { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Range(0, 8)]
        public int FreeSeats { get; set; }

        public string AdditionalComment { get; set; }

        public IEnumerable<TripDTO> UpcomingTrips { get; set; }
        public IEnumerable<TripDTO> PastTrips { get; set; }
        public IEnumerable<TripDriverDTO> ManageableTrips { get; set; }

    }
}
