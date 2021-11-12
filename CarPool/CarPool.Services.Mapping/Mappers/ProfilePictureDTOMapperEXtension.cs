using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.Mappers
{
    public static class ProfilePictureDTOMapperEXtension
    {
        public static ProfilePictureDTO GetDTO(this ProfilePicture picture)
        {
            if (picture is null || picture.ImageData is null || picture.Id <= 0 || picture.ApplicationUserId == default(Guid))
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA);
            }

            return new ProfilePictureDTO
            {
                Id = picture.Id,
                ApplicationUserId = picture.ApplicationUserId,
                ImageTitle = picture.ImageTitle,
                ImageData = picture.ImageData
            };
        }

        public static ProfilePicture GetEntity(this ProfilePictureDTO picture)
        {
            if (picture is null || picture.ImageData is null || picture.Id <= 0 || picture.ApplicationUserId == default(Guid))
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA);
            }

            return new ProfilePicture
            {
                Id = picture.Id,
                ApplicationUserId = picture.ApplicationUserId,
                ImageTitle = picture.ImageTitle,
                ImageData = picture.ImageData
            };
        }
    }
}
