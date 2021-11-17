using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.Mappers
{
    public static class AddressDTOMapperExtension
    {
        public static AddressDTO GetDTO(this Address address)
        {
            return new AddressDTO
            {
                AddressId = address.Id,
                CityId = address.CityId,
                CityName = address.City.Name,
                CountryId = address.City.Country.Id,
                CountryName = address.City.Country.Name,
                StreetName = address.StreetName,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };
        }

        public static Address GetModel(this AddressDTO addressDTO)
        {
            return new Address
            {
                CityId = addressDTO.CityId,
                StreetName = addressDTO.StreetName,
                Latitude = addressDTO.Latitude,
                Longitude = addressDTO.Longitude
            };
        }
    }
}
