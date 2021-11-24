using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Imagekit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public async Task<bool> UpdateAsync(string email, IFormFile image)
        {
            ServerImagekit imagekit = new ServerImagekit(GlobalConstants.ImageKitPublicKey,
                GlobalConstants.ImageKitPrivateKey,
                GlobalConstants.ImageKitUrlEndPoint);

            var imageName = Convert.ToBase64String(Encoding.ASCII.GetBytes(email)).Replace('=', '_');

            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var fileBytes = ms.ToArray();

                var uploadResp = await imagekit
                .FileName(imageName)
                .isPrivateFile(false)
                .UseUniqueFileName(false)
                .UploadAsync(fileBytes);

                var id = await _db.ApplicationUsers
                    .Where(x => x.Email == email)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
                var pic = await _db.ProfilePictures.FirstOrDefaultAsync(x => x.ApplicationUserId == id);
                pic.ImageLink = GlobalConstants.ImageKitUrlEndPoint + imageName;
                await _db.SaveChangesAsync();
            }
            return true;
        }
    }
}
