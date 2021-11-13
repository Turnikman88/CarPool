using CarPool.Services.Mapping.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ICityService : ICRUDshared<CityDTO>
    {
        Task<CityDTO> GetCityByIdAsync(int id);
        Task<CityDTO> GetCityByNameAsync(string name);
        Task<IEnumerable<CityDTO>> GetCitiesByNameAsync(int page, string name);
        Task<IEnumerable<CityDTO>> GetCitiesByCountryNameAsync(int page, string name);
    }
}
