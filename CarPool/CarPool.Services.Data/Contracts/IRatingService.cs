using CarPool.Services.Mapping.DTOs;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IRatingService
    {
        Task<RatingDTO> PostReportAsync(RatingDTO obj);

        Task<RatingDTO> PostFeedbackAsync(RatingDTO obj);
    }
}
