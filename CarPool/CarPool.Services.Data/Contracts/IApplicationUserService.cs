using CarPool.Services.Mapping.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserDisplayDTO>> FilterUsersAsync(int page, string part);

        Task<IEnumerable<ApplicationTopUserDTO>> TopUsers();

        Task<IEnumerable<ApplicationUserDisplayDTO>> GetAsync(int page);

        Task<ApplicationUserDTO> GetUserByEmailAsync(string email);

        Task<ApplicationUserDTO> PostAsync(ApplicationUserDTO obj);

        Task<ApplicationUserDTO> UpdateAsync(string email, ApplicationUserDTO obj);

        Task<ApplicationUserDTO> DeleteAsync(string email);

        Task<ApplicationUserDTO> UpdatePasswordAsync(string email, string newPassword);

    }
}
