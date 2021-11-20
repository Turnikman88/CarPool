using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IBanService
    {
        Task<IEnumerable<ApplicationUserDisplayDTO>> GetAllBannedUsersAsync(int page);
        Task<BanDTO> BanUserAsync(string email, string reason, DateTime? days);
        Task<BanDTO> UnbanUserAsync(string email);
    }
}
