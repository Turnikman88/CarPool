using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;

namespace CarPool.Services.Mapping.Mappers
{
    public static class RatingDTOMapperExtension
    {
        public static RatingDTO GetDTO(this Rating rating)
        {
            return new RatingDTO
            {
                Id = rating.Id,
                AddedByUserId = rating.AddedByUserId,
                ApplicationUserId = rating.ApplicationUserId,
                Feedback = rating.Feedback,
                Value = rating.Value,
                TripId = rating.TripId
            };
        }

        public static Rating GetModel(this RatingDTO rating)
        {
            return new Rating
            {
                Id = rating.Id,
                AddedByUserId = rating.AddedByUserId,
                ApplicationUserId = rating.ApplicationUserId,
                Feedback = rating.Feedback,
                Value = rating.Value,
                CreatedOn = System.DateTime.UtcNow,
                TripId = rating.TripId
            };
        }
    }
}
