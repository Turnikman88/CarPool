using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System.Linq;

namespace CarPool.Services.Mapping.Mappers
{
    public static class CityDTOMapperExtension
    {
        public static CityDTO GetDTO(this City city)
        {
            if (city is null || city.Name is null || city.Id <= 0 || city.CountryId <= 0 || city.Country.Name is null)
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA); ;
            }

            return new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
                CountryName = city.Country.Name,
                Addresses = city.Addresses.Select(x => x.StreetName).ToList()
            };
        }

        public static City GetEntity(this CityDTO city)
        {
            if (city is null || city.Name is null || city.Id < 0 || city.CountryId <= 0)
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA);
            }

            return new City
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId
            };
        }
    }
}
