using CarPool.Services.Mapping.DTOs;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IAddressService : ICRUDshared<AddressDTO>
    {
        Task<AddressDTO> GetAddressByIdAsync(int id);

        Task<AddressDTO> GetAddressByCountryCityNameAsync(string city, string country);

        Task<int> AddressToId(AddressDTO obj);
    }
}
