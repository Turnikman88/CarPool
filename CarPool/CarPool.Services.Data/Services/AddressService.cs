using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class AddressService : IAddressService
    {
        private readonly CarPoolDBContext _db;
        private readonly ICheckExistenceService _check;
        private readonly ICityService _city;
        private readonly ICountryService _country;


        public AddressService(CarPoolDBContext db, ICheckExistenceService check, ICityService city, ICountryService country)
        {
            _db = db;
            _check = check;
            _city = city;
            _country = country;
        }

        public async Task<IEnumerable<AddressDTO>> GetAsync(int page)
        {
            return await _db.Addresses
                .Include(c => c.City)
                .ThenInclude(c => c.Country)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x=>x.GetDTO()).ToListAsync();
        }

        public async Task<AddressDTO> GetByIdAsync(int id)
        {
            _check.CheckId(id);

            var result = await _db.Addresses
                .Include(c => c.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(x=>x.Id == id);

            return result != null ? result.GetDTO() : new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_NOT_FOUND };
        }


        public async Task<AddressDTO> PostAsync(AddressDTO obj)
        {
            if(!await _check.AddressExistsAsync(obj.StreetName, obj.CityName, obj.CountryId))
                return new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_EXISTS };

            //_ = await _check.AddressExistsAsync(obj.StreetName, obj.CityName, obj.CountryId) 
            //    == true ? throw new AppException(GlobalConstants.ADDRESS_EXISTS) : 0;

            AddressDTO result = null;

            var deletedAddress = await _db.Addresses.Include(x => x.City).ThenInclude(x => x.Country).IgnoreQueryFilters().FirstOrDefaultAsync(x => x.CityId == obj.CityId && x.StreetName == obj.StreetName && x.City.Country.Name == obj.CountryName && x.IsDeleted == true);

            var newAddress = obj.GetModel();

            if(deletedAddress == null)
            {
                await this._db.Addresses.AddAsync(newAddress);
                await _db.SaveChangesAsync();
                newAddress = await _db.Addresses.Include(x => x.City).ThenInclude(x => x.Country).FirstOrDefaultAsync(x => x.Id == newAddress.Id);
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
            var model = await GetByIdAsync(id);
            var addressToUpdate = model.GetModel();
            var city = _city.GetCityByNameAsync(obj.CityName);
            var country = _country.GetCountryByNameAsync(obj.CountryName);

            addressToUpdate.CityId = city.Id;
            addressToUpdate.StreetName = obj.StreetName;
            addressToUpdate.Latitude = obj.Latitude;
            addressToUpdate.Longitude = obj.Longitude;
            addressToUpdate.City.CountryId = country.Id;

            await _db.SaveChangesAsync();

            return addressToUpdate.GetDTO();
        }

        public async Task<AddressDTO> DeleteAsync(int id)
        {
            _check.CheckId(id);

            var address = await this._db.Addresses.FirstOrDefaultAsync(x => x.Id == id); //?? throw new AppException(GlobalConstants.ADDRESS_NOT_FOUND);
            
            if(address is null)
                return new AddressDTO() { ErrorMessage = GlobalConstants.ADDRESS_NOT_FOUND };
            
            address.DeletedOn = System.DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return address.GetDTO();
        }

    }
}
