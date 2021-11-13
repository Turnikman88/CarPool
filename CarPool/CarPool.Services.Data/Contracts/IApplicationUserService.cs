using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserDisplayDTO>> FilterUsersAsync(int page, string part);
        Task<IEnumerable<ApplicationUserDisplayDTO>> GetAsync(int page);
        Task<ApplicationUserDTO> PostAsync(ApplicationUserDTO obj);
        Task<ApplicationUserDTO> UpdateAsync(Guid id, ApplicationUserDTO obj);
        Task<ApplicationUserDTO> DeleteAsync(Guid id);
        Task<ApplicationUserDisplayDTO> BanUserAsync(Guid id, DateTime? due);
        Task RemoveBanAsync();
    }
}
