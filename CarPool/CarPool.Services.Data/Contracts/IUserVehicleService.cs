using CarPool.Services.Mapping.DTOs;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IUserVehicleService
    {
        Task<UserVehicleDTO> PostAsync(UserVehicleDTO obj);

        Task<UserVehicleDTO> UpdateAsync(int id, UserVehicleDTO obj);

        Task<UserVehicleDTO> DeleteAsync(int id);

        Task<UserVehicleDTO> GetUserVehicle(string email);
    }
}
