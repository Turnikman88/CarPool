using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ICheckExistenceService
    {
        Task<bool> CityExists(string name, int countryId);
        Task<bool> CountryExists(string name);
    }
}