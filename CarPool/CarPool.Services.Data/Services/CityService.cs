using CarPool.Common;
using CarPool.Common.Exceptions;
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
    public class CityService : ICityService
    {
        private readonly CarPoolDBContext _db;
        private readonly ICheckExistenceService _check;

        public CityService(CarPoolDBContext db, ICheckExistenceService check)
        {
            this._db = db;
            this._check = check;
        }

        public async Task<IEnumerable<CityDTO>> GetAsync(int page)
        {
            return await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<CityDTO> GetCityByIdAsync(int id)
        {
            _check.CheckId(id);
            var city = await _db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (city is null)
                return new CityDTO() { ErrorMessage = GlobalConstants.CITY_NOT_FOUND };

            return city.GetDTO();
        }


        public async Task<CityDTO> GetCityByNameAsync(string name)
        {
            var city = await _db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

            if (city is null)
                return new CityDTO() { ErrorMessage = GlobalConstants.CITY_NOT_FOUND };

            return city.GetDTO();
        }

        public async Task<IEnumerable<CityDTO>> GetCitiesByPartNameAsync(int page, string name)
        {
            return await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<IEnumerable<CityDTO>> GetCitiesByCountryNameAsync(int page, string name)
        {
            return await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .Where(x => x.Country.Name.ToLower().Contains(name.ToLower()))
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<CityDTO> PostAsync(CityDTO obj)
        {
            // _ = await _check.CityExistsAsync(obj.Name, obj.CountryId)
            //     == true ? throw new AppException(GlobalConstants.CITY_EXISTS) : 0;

            if (await _check.CityExistsAsync(obj.Name, obj.CountryId))
                return new CityDTO() { ErrorMessage = GlobalConstants.CITY_EXISTS };

            CityDTO result = null;

            var deletedCity = await _db.Cities.Include(x => x.Country).IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.CountryId == obj.CountryId && x.Name == obj.Name && x.IsDeleted == true);

            if (obj is null || obj.Name is null || obj.Id < 0 || obj.CountryId <= 0)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            var newCity = obj.GetEntity();
            if (deletedCity == null)
            {
                await this._db.Cities.AddAsync(newCity);
                await _db.SaveChangesAsync();
                newCity = await _db.Cities.Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == newCity.Id);
                result = newCity.GetDTO();
            }
            else
            {
                deletedCity.DeletedOn = null;
                deletedCity.IsDeleted = false;
                await _db.SaveChangesAsync();
                result = deletedCity.GetDTO();
            }

            return result;
        }

        public async Task<CityDTO> UpdateAsync(int id, CityDTO obj)
        {
            _check.CheckId(id);

            if (await _check.CityExistsAsync(obj.Name, obj.CountryId))
                return new CityDTO() { ErrorMessage = GlobalConstants.CITY_EXISTS };


            var city = await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (city is null)
                return new CityDTO() { ErrorMessage = GlobalConstants.CITY_NOT_FOUND };

            if (obj.Name is null)
                return new CityDTO() { ErrorMessage = GlobalConstants.INCORRECT_DATA };


            city.Name = obj.Name;
            city.CountryId = obj.CountryId;
            await _db.SaveChangesAsync();

            return city.GetDTO();
        }

        public async Task<CityDTO> DeleteAsync(int id)
        {
            _check.CheckId(id);

            var city = await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (city is null)
                return new CityDTO() { ErrorMessage = GlobalConstants.CITY_NOT_FOUND };

            city.DeletedOn = System.DateTime.Now;
            this._db.Cities.Remove(city);
            await _db.SaveChangesAsync();

            return city.GetDTO();
        }

    }
}
