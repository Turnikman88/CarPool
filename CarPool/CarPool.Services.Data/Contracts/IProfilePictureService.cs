using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IProfilePictureService
    {
        public Task<bool> UpdateAsync(string email, IFormFile image);

        Task<string> GetPictureLinkByUserEmail(string email);
    }
}
