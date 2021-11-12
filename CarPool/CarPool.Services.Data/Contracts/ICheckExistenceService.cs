using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ICheckExistenceService
    {
        Task<bool> AddressExistsAsync(string addressStreet, string cityName, int countryId);
        Task<bool> CityExistsAsync(string name, int countryId);
        Task<bool> CountryExistsAsync(string name);
        void CheckId(int id);
    }
}