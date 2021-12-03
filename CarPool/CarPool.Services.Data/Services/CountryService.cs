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
    public class CountryService : ICountryService
    {
        private readonly CarPoolDBContext _db;

        public CountryService(CarPoolDBContext db)
        {
            _db = db;
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
                .Take(10)
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

        public async Task<IEnumerable<CountryDTO>> RenderCountryListAsync()
        {
            return await _db.Countries
                .Include(c => c.Cities)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<CountryDTO> PostAsync(CountryDTO obj)
        {
            if (obj is null || string.IsNullOrEmpty(obj.Name))
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            if (await _db.Countries.AnyAsync(x => x.Name == obj.Name))
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_EXISTS };
            }

            var newCountry = obj.GetEntity();
            var deletedCountry = await _db.Countries.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name == obj.Name && x.IsDeleted == true);

            if (deletedCountry == null)
            {
                await _db.Countries.AddAsync(newCountry);
                await _db.SaveChangesAsync();
                obj.Id = newCountry.Id;
            }
            else
            {
                deletedCountry.DeletedOn = null;
                deletedCountry.IsDeleted = false;
                await _db.SaveChangesAsync();
                obj.Id = deletedCountry.Id;
            }

            return obj;
        }

        public async Task<CountryDTO> UpdateAsync(int id, CountryDTO obj)
        {
            if(await _db.Countries.FirstOrDefaultAsync(x => x.Name == obj.Name) != null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_EXISTS };
            }

            if (string.IsNullOrEmpty(obj.Name))
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };

            }

            var model = await _db.Countries.Include(c => c.Cities).FirstOrDefaultAsync(x => x.Id == id);

            if (model is null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_NOT_FOUND };

            }

            model.Name = obj.Name;
            await _db.SaveChangesAsync();

            return model.GetDTO();
        }

        public async Task<CountryDTO> DeleteAsync(int id)
        {
            var model = await _db.Countries.Include(c => c.Cities).FirstOrDefaultAsync(x => x.Id == id);
            if (model is null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_NOT_FOUND };
            }

            model.DeletedOn = System.DateTime.Now;
            _db.Countries.Remove(model);
            await _db.SaveChangesAsync();

            return model.GetDTO();
        }
    }
}
