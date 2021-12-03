using CarPool.Common;
using CarPool.Data;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class AddressService : IAddressService
    {
        private readonly CarPoolDBContext _db;
        private readonly ICheckExistenceService _check;
        private readonly ICityService _city;
        private readonly ICountryService _country;
        private readonly IBingApiService _bing;

        public AddressService(CarPoolDBContext db, ICheckExistenceService check, ICityService city, ICountryService country, IBingApiService bing)
        {
            _db = db;
            _check = check;
            _city = city;
            _country = country;
            _bing = bing;
        }

        public async Task<IEnumerable<AddressDTO>> GetAsync(int page)
        {
            return await _db.Addresses
                .Include(c => c.City)
                .ThenInclude(c => c.Country)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<AddressDTO> GetAddressByIdAsync(int id)
        {
            _check.CheckId(id);

            var result = await _db.Addresses
                                 .Include(c => c.City)
                                 .ThenInclude(c => c.Country)
                                 .FirstOrDefaultAsync(x => x.Id == id);

            return result != null ? result.GetDTO() : new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_NOT_FOUND };
        }

        public async Task<AddressDTO> GetAddressByCountryCityNameAsync(string city, string country)
        {
            var result = await _db.Addresses
                                 .Include(c => c.City)
                                 .ThenInclude(c => c.Country)
                                 .FirstOrDefaultAsync(x => x.City.Name == city && x.City.Country.Name == country);

            return result != null ? result.GetDTO() : new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_NOT_FOUND };
        }

        public async Task<int> AddressToId(AddressDTO obj)
        {
            var address = await _db.Addresses
                                 .Include(c => c.City)
                                 .ThenInclude(c => c.Country)
                                 .FirstOrDefaultAsync(x => x.City.Country.Name == obj.CountryName
                                 && x.City.Name == obj.CityName
                                 && x.StreetName == obj.StreetName);

            if (address != null)
            {
                return address.Id;
            }
            await AddressAssignData(obj);
            var postNewAddress = await PostAsync(obj);
            return postNewAddress.AddressId;
        }

        public async Task<AddressDTO> PostAsync(AddressDTO obj)
        {
            if (await _check.AddressExistsAsync(obj.StreetName, obj.CityName, obj.CountryId))
                return new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_EXISTS };

            AddressDTO result = null;

            var deletedAddress = await _db.Addresses.Include(x => x.City).ThenInclude(x => x.Country)
                                                    .IgnoreQueryFilters()
                                                    .FirstOrDefaultAsync(x => x.CityId == obj.CityId
                                                    && x.StreetName == obj.StreetName
                                                    && x.City.Country.Name == obj.CountryName
                                                    && x.IsDeleted == true);

            await AddressAssignData(obj);

            var newAddress = obj.GetModel();

            if (deletedAddress == null)
            {
                await _db.Addresses.AddAsync(newAddress);
                await _db.SaveChangesAsync();
                newAddress = await _db.Addresses.Include(x => x.City).ThenInclude(x => x.Country)
                                                .FirstOrDefaultAsync(x => x.Id == newAddress.Id);
                result = newAddress.GetDTO();
            }
            else
            {
                deletedAddress.DeletedOn = null;
                deletedAddress.IsDeleted = false;
                await _db.SaveChangesAsync();
                result = deletedAddress.GetDTO();
            }

            return result;
        }

        public async Task<AddressDTO> UpdateAsync(int id, AddressDTO obj)
        {
            await AddressAssignData(obj);

            var modelToUpdate = await _db.Addresses.Include(c => c.City)
                                                   .ThenInclude(c => c.Country)
                                                   .FirstOrDefaultAsync(x => x.Id == id);

            if (modelToUpdate is null)
                return new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_NOT_FOUND };

            modelToUpdate.CityId = obj.CityId;
            modelToUpdate.StreetName = obj.StreetName;
            modelToUpdate.Latitude = obj.Latitude;
            modelToUpdate.Longitude = obj.Longitude;

            await _db.SaveChangesAsync();

            return await GetAddressByIdAsync(id);
        }

        public async Task<AddressDTO> DeleteAsync(int id)
        {
            _check.CheckId(id);

            var address = await _db.Addresses
                                        .Include(c => c.City)
                                        .ThenInclude(c => c.Country)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (address is null || address.IsDeleted)
                return new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_NOT_FOUND };

            address.DeletedOn = System.DateTime.UtcNow;
            address.IsDeleted = true;
            await _db.SaveChangesAsync();

            return address.GetDTO();
        }

        private async Task<AddressDTO> AddressAssignData(AddressDTO obj)
        {
            var cityDetails = await _city.GetCityByNameAsync(obj.CityName);
            var countryDetails = await _country.GetCountryByNameAsync(obj.CountryName);

            if (cityDetails.ErrorMessage != null || countryDetails.ErrorMessage != null)
            {
                if (countryDetails.ErrorMessage != null)
                {
                    var newCountry = await _country.PostAsync(new CountryDTO { Name = obj.CountryName });
                    countryDetails.Id = newCountry.Id;
                }

                await _city.PostAsync(new CityDTO { CountryName = obj.CountryName, Name = obj.CityName });
                await _db.SaveChangesAsync();
                cityDetails = await _city.GetCityByNameAsync(obj.CityName);
            }

            var coordinates = await _bing.GetLatitudeAndLongitude(obj.CityName, obj.CountryName, obj.StreetName);
            obj.Latitude = coordinates.Item1;
            obj.Longitude = coordinates.Item2;
            obj.CityId = cityDetails.Id;
            obj.CountryId = countryDetails.Id;
            return obj;
        }
    }
}
