using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IGoogleAccountService
    {
        Task AddGoogleAccount(string email);

        Task DeleteGoogleAccount(string email);

        Task<bool> IsGoogleAccount(string email);
    }
}