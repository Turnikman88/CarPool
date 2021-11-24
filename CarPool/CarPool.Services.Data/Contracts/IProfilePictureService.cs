using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IProfilePictureService
    {
        public Task<bool> UpdateAsync(string email, IFormFile image);
    }
}
