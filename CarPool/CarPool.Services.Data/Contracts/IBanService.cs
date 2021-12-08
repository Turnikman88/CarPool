using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IBanService
    {
        Task<IEnumerable<BanDTO>> GetAllBannedUsersAsync(int page);

        Task<BanDTO> BanUserAsync(string email, string reason, DateTime? days);

        Task<BanDTO> UnbanUserAsync(string email);

        Task<IEnumerable<ReportedDTO>> GetTopReportedUsersAsync();

        Task<ReportedDTO> GetReportedUserByEmailAsync(string email);

        Task IgnoreReportAsync(string email);

        Task<int> GetMaxPageAsync();
    }
}
