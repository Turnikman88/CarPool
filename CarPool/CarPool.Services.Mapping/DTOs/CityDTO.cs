using CarPool.Services.Mapping.Contracts;
using System.Collections.Generic;

namespace CarPool.Services.Mapping.DTOs
{
    public class CityDTO : IErrorMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string ErrorMessage { get; set; }

        public List<string> Addresses = new List<string>();
    }
}