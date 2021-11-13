using CarPool.Services.Mapping.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAsync(int page);
        Task<CountryDTO> GetCountryByIdAsync(int id);
        Task<CountryDTO> GetCountryByNameAsync(string name);
        Task<IEnumerable<CountryDTO>> GetCountriesByPartNameAsync(int page, string part);
    }
}
