using CarPool.Common;
using CarPool.Data;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class RatingService : IRatingService
    {
        private readonly CarPoolDBContext _db;

        public RatingService(CarPoolDBContext db)
        {
            this._db = db;
        }

        public async Task<bool> IsAlreadyRatedAsync(string driverID, string userID, int tripID)
        {
            return await this._db.Ratings.AnyAsync(x => x.AddedByUserId.ToString() == userID && x.ApplicationUserId.ToString() == driverID && x.TripId == tripID);
        }

        public async Task<RatingDTO> PostFeedbackAsync(RatingDTO obj)
        {
            if (!await IsAlreadyRatedAsync(obj.ApplicationUserId.ToString(), obj.AddedByUserId.ToString(), obj.TripId))
            {
                return new RatingDTO { ErrorMessage = GlobalConstants.TRIP_ALREADY_REVIEWED };
            }

            if (obj.ApplicationUserId == obj.AddedByUserId)
            {
                return new RatingDTO { ErrorMessage = GlobalConstants.TRIP_YOU_CANNOT_REVEIW_YOURSELF };
            }

            var model = obj.GetModel();

            await _db.Ratings.AddAsync(model);
            await _db.SaveChangesAsync();

            return model.GetDTO();
        }
    }
}
