using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public class CheckExistenceService : ICheckExistenceService
    {
        private readonly CarPoolDBContext _db;

        public CheckExistenceService(CarPoolDBContext db)
        {
            _db = db;
        }

        public async Task<bool> CountryExistsAsync(string name)
        {
            return await _db.Countries.AnyAsync(x => x.Name == name);
        }

        public void CheckId(int id)
        {
            _ = id < 0 ? throw new AppException(GlobalConstants.INVALID_ID) : 0;
        }

        public async Task<bool> CityExistsAsync(string name, int countryId)
        {
            return await _db.Cities.AnyAsync(x => x.Name == name && x.CountryId == countryId);
        }

        public async Task<bool> AddressExistsAsync(string addressStreet, string cityName, int countryId)
        {
            return await _db.Addresses.AnyAsync(x => x.StreetName == addressStreet && x.City.Name == cityName && x.City.CountryId == countryId);
        }

        public async Task<bool> FindAddressByIDAsync(int id)
        {
            return await _db.Addresses.AnyAsync(x => x.Id == id);

        }
    }
}
