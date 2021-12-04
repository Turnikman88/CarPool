using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.Mappers
{
    public static class BanDTOMapperExtension
    {
        public static BanDTO GetBanDTO(this ApplicationUser user)
        {
            return new BanDTO
            {
                Picture = user.ProfilePicture.ImageLink,
                BlockedDue = user.Ban?.BlockedDue,
                UserEmail = user.Email,
                BlockedOn = user.Ban?.BlockedOn,
                Reason = user.Ban?.Reason
            };
        }
    }
}
