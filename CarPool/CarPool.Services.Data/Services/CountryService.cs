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
using JW;
namespace CarPool.Services.Data.Services
{
    public class CountryService : ICountryService
    {
        private readonly CarPoolDBContext _db;

        public CountryService(CarPoolDBContext db)
        {
            this._db = db;
        }
        public async Task<CountryDTO> GetCountryByIdAsync(int id)
        {
            return await _db.Countries
                .Where(x => x.Id == id)
                .Include(c => c.Cities)
                .Select(x => x.GetDTO())
                .FirstOrDefaultAsync() ?? new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_NOT_FOUND };
        }

        public async Task<CountryDTO> GetCountryByNameAsync(string name)
        {
            return await _db.Countries
                .Include(c => c.Cities)
                .Where(x => x.Name == name)
                .Select(x => x.GetDTO())
                .FirstOrDefaultAsync() ?? new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_NOT_FOUND };
        }

        public async Task<IEnumerable<CountryDTO>> GetCountriesByPartNameAsync(int page, string part)
        {
            return await _db.Countries
                .Where(x => x.Name.Contains(part))
                .Include(c => c.Cities)
                .Skip(page * GlobalConstants.PageSkip)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<IEnumerable<CountryDTO>> GetAsync(int page)
        {
            
            return await _db.Countries
                .Include(c => c.Cities)
                .Skip(page * GlobalConstants.PageSkip)
                .Select(x => x.GetDTO())
                .Take(10)
                .ToListAsync();
        }              
    }
}
