using CarPool.Common;
using CarPool.Data;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class UserVehicleService : IUserVehicleService
    {
        private readonly CarPoolDBContext _db;
        private readonly IAuthService _auth;

        public UserVehicleService(CarPoolDBContext db)
        {
            this._db = db;
        }

        public async Task<UserVehicleDTO> PostAsync(UserVehicleDTO obj)
        {
            if (obj is null || string.IsNullOrEmpty(obj.Model)
                || string.IsNullOrEmpty(obj.Color)
                || obj.FuelConsumptionPerHundredKilometers <= 0)
            {
                return new UserVehicleDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            var newVehicle = obj.GetEntity();

            var deletedVehicle = await _db.UserVehicles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Color == obj.Color
            && x.Model == obj.Model
            && x.IsDeleted == true);

            if (deletedVehicle == null)
            {
                await this._db.UserVehicles.AddAsync(newVehicle);
                await _db.SaveChangesAsync();
                obj.Id = newVehicle.Id;
            }
            else
            {
                deletedVehicle.DeletedOn = null;
                deletedVehicle.IsDeleted = false;
                await _db.SaveChangesAsync();
                obj.Id = deletedVehicle.Id;
            }

            return obj;
        }

        public async Task<UserVehicleDTO> UpdateAsync(int id, UserVehicleDTO obj)
        {
            if (obj is null)
            {
                return new UserVehicleDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            if (!await _db.UserVehicles.AnyAsync(x => x.Id == id))
            {
               return await PostAsync(obj);
            }

            var model = await this._db.UserVehicles.FirstOrDefaultAsync(x => x.Id == id);

            MapVehicle(obj, model);

            await _db.SaveChangesAsync();

            return model.GetDTO();
        }

        private static void MapVehicle(UserVehicleDTO obj, CarPool.Data.Models.DatabaseModels.UserVehicle model)
        {
            if (obj.Color != null)
            {
                model.Color = obj.Color;
            }

            if (obj.Model != null)
            {
                model.Model = obj.Model;
            }

            if (obj.FuelConsumptionPerHundredKilometers > 0)
            {
                model.FuelConsumptionPerHundredKilometers = obj.FuelConsumptionPerHundredKilometers;
            }
        }

        public async Task<UserVehicleDTO> DeleteAsync(int id)
        {
            var model = await _db.UserVehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (model is null)
            {
                return new UserVehicleDTO { ErrorMessage = GlobalConstants.VEHICLE_NOT_FOUND };
            }

            model.DeletedOn = System.DateTime.Now;
            this._db.UserVehicles.Remove(model);
            await _db.SaveChangesAsync();

            return model.GetDTO();
        }

        public async Task<UserVehicleDTO> GetUserVehicle(string email)
        {
            var userId = await _db.ApplicationUsers
                .Where(x => x.Email == email)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (userId == null)
            {
                return new UserVehicleDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            var model = await _db.UserVehicles.FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (model is null)
            {
                return new UserVehicleDTO { ErrorMessage = GlobalConstants.NO_VEHICLES };
            }
           
            return model.GetDTO();
        }
    }
}
