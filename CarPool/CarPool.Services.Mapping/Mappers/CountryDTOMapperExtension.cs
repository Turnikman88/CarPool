using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System.Linq;

namespace CarPool.Services.Mapping.Mappers
{
    public static class CountryDTOMapperExtension
    {
        public static CountryDTO GetDTO(this Country country)
        {
            if (country is null || string.IsNullOrEmpty(country.Name) || country.Cities == null)
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA);
            }
            return new CountryDTO
            {
                Id = country.Id,
                Name = country.Name,
                Cities = country.Cities.Select(c => c.Name).ToList()
            };
        }

        public static Country GetEntity(this CountryDTO country)
        {
            if (country is null || string.IsNullOrEmpty(country.Name))
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA);
            }
            return new Country
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}
