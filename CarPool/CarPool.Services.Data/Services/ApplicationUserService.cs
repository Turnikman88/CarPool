using AutoMapper;
using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarPool.Services.Data.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly CarPoolDBContext _db;

        public ApplicationUserService(CarPoolDBContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<ApplicationUserDisplayDTO>> FilterUsersAsync(int page, string part)
        {
            return await _db.ApplicationUsers.Where(x => x.Email.Contains(part) 
            || x.PhoneNumber.Contains(part) 
            || x.Username.Contains(part))
                .Include(x => x.Address)
                .Include(x => x.Ratings)
                .Include(x => x.Trips)
                .Include(x => x.Vehicle)
                .Include(x => x.Ban)
                .Skip(page * GlobalConstants.PageSkip)
                .Select(x => x.GetDisplayDTO())
                .ToListAsync();
        }

        public async Task<ApplicationUserDTO> DeleteAsync(Guid id)
        {
            var user = await _db.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(GlobalConstants.USER_NOT_FOUND);

            var userDTO = user.GetDTO();

            user.DeletedOn = DateTime.Now;
            _db.ApplicationUsers.Remove(user);
            await _db.SaveChangesAsync();

            return userDTO;
        }

        public async Task<IEnumerable<ApplicationUserDisplayDTO>> GetAsync(int page)
        {
            return await _db.ApplicationUsers
                .Include(x => x.Address)
                .Include(x => x.Ratings)
                .Include(x => x.Trips)
                .Include(x => x.Vehicle)
                .Include(x => x.Ban)
                .Skip(page * GlobalConstants.PageSkip)
                .Select(x => x.GetDisplayDTO())
                .ToListAsync();
        }

        public async Task<ApplicationUserDTO> PostAsync(ApplicationUserDTO obj)
        {
            ApplicationUserDTO result = null;

            var newUser = obj.GetEntity();

            var deleteUser = await _db.ApplicationUsers.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Email == newUser.Email && x.IsDeleted == true);

            if (deleteUser == null)
            {
                if (!IsValidUser(obj.Username, obj.Email,
                obj.EmailConfirmed, obj.Password, obj.PhoneNumber))
                {
                    throw new AppException(GlobalConstants.INCORRECT_DATA);
                }

                await _db.ApplicationUsers.AddAsync(newUser);
                await _db.SaveChangesAsync();
                newUser = await _db.ApplicationUsers
                    .FirstOrDefaultAsync(x => x.Id == newUser.Id);

                result = newUser.GetDTO();
            }
            else
            {
                deleteUser.DeletedOn = null;
                deleteUser.IsDeleted = false;
                await _db.SaveChangesAsync();
                result = deleteUser.GetDTO();
            }

            return result;
        }

        public async Task<ApplicationUserDTO> UpdateAsync(Guid id, ApplicationUserDTO obj)
        {
            _ = await _db.ApplicationUsers.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Email == obj.Email)
                != null ? throw new AppException(GlobalConstants.USER_EXISTS) : 0;

            var user = await _db.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(GlobalConstants.USER_NOT_FOUND);

            MapUser(obj, user);

            await _db.SaveChangesAsync();

            return user.GetDTO();
        }

        public async Task<ApplicationUserDisplayDTO> BanUserAsync(Guid id, DateTime? due)
        {
            var user = await _db.ApplicationUsers
               .FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new AppException(GlobalConstants.USER_NOT_FOUND);

            user.Ban.BlockedOn = DateTime.UtcNow.Date;
            user.Ban.BlockedDue = due;

            await _db.SaveChangesAsync();

            return user.GetDisplayDTO();
        }

        public async Task RemoveBanAsync()
        {
            await _db.ApplicationUsers.Include(x => x.Ban)
                .Where(x => x.Ban.BlockedDue < DateTime.UtcNow.Date)
                .ForEachAsync(x => { x.Ban.BlockedOn = null; x.Ban.BlockedDue = null; });

            await _db.SaveChangesAsync();            
        }

        private static void MapUser(ApplicationUserDTO obj, ApplicationUser user)
        {
            if (obj.Username != null && obj.Username.Length >= 2 && obj.Username.Length <= 20)
            {
                user.Username = obj.Username;
            }

            if (obj.FirstName != null)
            {
                user.FirstName = obj.FirstName;
            }

            if (obj.LastName != null)
            {
                user.LastName = obj.FirstName;
            }

            if (obj.Email != null && Regex.IsMatch(obj.Email, @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+"))
            {
                user.Email = obj.Email;
            }

            if (obj.Password != null && Regex.IsMatch(obj.Password, GlobalConstants.PassRegex))
            {
                user.Password = obj.Password;
            }

            if (obj.PhoneNumber != null && Regex.IsMatch(obj.PhoneNumber, GlobalConstants.PhoneRegex))
            {
                user.PhoneNumber = obj.PhoneNumber;
            }
        }

        private bool IsValidUser(string username, string email, bool emailConfirmed, string password, string phoneNumber)
        {
            var validUsername = username.Length >= 2 && username.Length <= 20;
            var validEmail = Regex.IsMatch(email, @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+");
            var validPassword = Regex.IsMatch(password, GlobalConstants.PassRegex);
            var validPhone = Regex.IsMatch(phoneNumber, GlobalConstants.PhoneRegex);
            return validUsername && validEmail && validPassword && validPhone;

        }
    }
}
