using CarPool.Services.Mapping.DTOs;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IRatingService
    {
        Task<bool> IsAlreadyRatedAsync(string driverID, string userID, int tripID);

        Task<RatingDTO> PostFeedbackAsync(RatingDTO obj);
    }
}
