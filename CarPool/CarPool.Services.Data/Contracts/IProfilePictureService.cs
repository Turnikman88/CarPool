using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IProfilePictureService
    {
        Task<ProfilePictureDTO> PostAsync(ProfilePictureDTO obj);
        Task<ProfilePictureDTO> UpdateAsync(int id, ProfilePictureDTO obj);
        Task<ProfilePictureDTO> DeleteAsync(int id);
    }
}
