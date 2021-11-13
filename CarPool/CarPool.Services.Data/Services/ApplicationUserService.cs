using AutoMapper;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly CarPoolDBContext _db;

        public ApplicationUserService(CarPoolDBContext db)
        {
            this._db = db;
        }

        public Task<ApplicationUserDTO> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetAsync()
        {
            //return await _db.ApplicationUsers.
            throw new NotImplementedException();

        }

        public Task<ApplicationUserDTO> PostAsync(ApplicationUserDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUserDTO> UpdateAsync(int id, ApplicationUserDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
