using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class AddressDTO
    {
        public int CityId { get; set; }

        public string CityName { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string StreetName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
