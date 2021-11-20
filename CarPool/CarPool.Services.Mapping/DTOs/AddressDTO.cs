using CarPool.Services.Mapping.Contracts;

namespace CarPool.Services.Mapping.DTOs
{
    public class AddressDTO : IErrorMessage
    {
        public int AddressId { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string StreetName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string ErrorMessage { get; set; }

    }
}
