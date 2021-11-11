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
            this._db = db;
        }

        public async Task<bool> CountryExists(string name)
        {
            return await _db.Countries.AnyAsync(x => x.Name == name);
        }

        public async Task<bool> CityExists(string name, int countryId)
        {
            return await _db.Cities.AnyAsync(x => x.Name == name && x.CountryId == countryId);
        }               
    }
}
