using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPool.Services.Mapping.Mappers
{
    public static class ReportedDTOMapperExtension
    {
        public static ReportedDTO GetReportedDTO(this Rating rating)
        {
            return new ReportedDTO
            {
                Email = rating.ApplicationUser.Email,
                Reason = rating.Feedback,
                Picture = rating.ApplicationUser.ProfilePicture.ImageLink
            };
        }
    }
}
