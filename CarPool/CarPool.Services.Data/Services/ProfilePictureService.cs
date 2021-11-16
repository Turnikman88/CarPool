using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class ProfilePictureService : IProfilePictureService
    {
        private readonly CarPoolDBContext _db;

        public ProfilePictureService(CarPoolDBContext db)
        {
            this._db = db;
        }

        public async Task<ProfilePictureDTO> DeleteAsync(int id)
        {
            var picture = await this._db.ProfilePictures
                .FirstOrDefaultAsync(x => x.Id == id);
                
            if (picture is null)
            {
                return new ProfilePictureDTO { ErrorMessage = GlobalConstants.PROFILE_PICTURE_NOT_FOUND };
            }

            picture.DeletedOn = System.DateTime.Now;
            this._db.ProfilePictures.Remove(picture);
            await _db.SaveChangesAsync();

            return picture.GetDTO();

        }

        public async Task<ProfilePictureDTO> PostAsync(ProfilePictureDTO obj)
        {
            if (obj is null || obj.ImageData is null || obj.Id <= 0 || obj.ApplicationUserId == default(Guid))
            {
                return new ProfilePictureDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            if (await _db.ProfilePictures.FirstOrDefaultAsync(x => x.ImageData == obj.ImageData) != null)
            {
                return new ProfilePictureDTO { ErrorMessage = GlobalConstants.PICTURE_EXISTS };
            }

            var newPicture = obj.GetEntity();
            var deletedPicture = await _db.ProfilePictures
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.ImageData == obj.ImageData && x.IsDeleted == true);

            if (deletedPicture == null)
            {
                await this._db.ProfilePictures.AddAsync(newPicture);
                await _db.SaveChangesAsync();
                obj.Id = newPicture.Id;
            }
            else
            {
                deletedPicture.DeletedOn = null;
                deletedPicture.IsDeleted = false;
                await _db.SaveChangesAsync();
                obj.Id = deletedPicture.Id;
            }

            return obj;
        }

        public async Task<ProfilePictureDTO> UpdateAsync(int id, ProfilePictureDTO obj)
        {
            if(await _db.ProfilePictures.FirstOrDefaultAsync(x => x.ImageData == obj.ImageData) != null)
            {
                return new ProfilePictureDTO { ErrorMessage = GlobalConstants.PICTURE_EXISTS };
            }

            if (obj.ImageData.Length == 0)
            {
                return new ProfilePictureDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            var picture = await this._db.ProfilePictures
                .FirstOrDefaultAsync(x => x.Id == id);
            if (picture is null)
            {
                return new ProfilePictureDTO { ErrorMessage = GlobalConstants.PROFILE_PICTURE_NOT_FOUND };
            }

            picture.ImageData = obj.ImageData;
            await _db.SaveChangesAsync();

            return picture.GetDTO();
        }
    }
}
