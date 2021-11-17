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
                    .ThenInclude(x => x.DestinationAddress)
                .Include(x => x.Vehicle)
                .Include(x => x.Ban)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDisplayDTO())
                .ToListAsync();
        }

        public async Task<ApplicationUserDTO> DeleteAsync(Guid id)
        {
            var user = await _db.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            var userDTO = user.GetDTO();

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
                    .ThenInclude(x => x.DestinationAddress)
                .Include(x => x.Vehicle)
                .Include(x => x.Ban)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDisplayDTO())
                .ToListAsync();
        }

        public async Task<ApplicationUserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _db.ApplicationUsers
                .Include(x => x.Address)
                .Include(x => x.Ratings)
                .Include(x => x.Trips)
                .Include(x => x.Vehicle)
                .Include(x => x.ApplicationRole)
                .Include(x => x.Ban)
                .Where(x => x.Email == email)
                .Select(x => x.GetDTO())
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            return user;

        }

        public async Task<ApplicationUserDTO> PostAsync(ApplicationUserDTO obj)
        {
            if (obj is null || obj.Username is null || obj.FirstName is null
                || obj.LastName is null || obj.Email is null
                || !Regex.IsMatch(obj.PhoneNumber ?? "", GlobalConstants.PhoneRegex)
                || obj.Password is null)
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            if (await _db.ApplicationUsers.AnyAsync(x => x.Email == obj.Email))
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_EXISTS };
            }

            if (await _db.ApplicationUsers.AnyAsync(x => x.PhoneNumber == obj.PhoneNumber))
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_PHONE_EXISTS };
            }

            if (!await _db.Addresses.AnyAsync(x => x.Id == obj.AddressId))
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.ADDRESS_NOT_FOUND };
            }

            var newUser = obj.GetEntity();

            if (!IsValidUser(obj.Username, obj.Email,
            obj.Password, obj.PhoneNumber))
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            await _db.ApplicationUsers.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return await _db.ApplicationUsers
                .Include(x => x.ApplicationRole)
                .Where(x => x.Id == newUser.Id)
                .Select(x => x.GetDTO())
                .FirstOrDefaultAsync();           
        }

        public async Task<ApplicationUserDTO> UpdateAsync(Guid id, ApplicationUserDTO obj)
        {
            if (await _db.ApplicationUsers.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Email == obj.Email) != null)
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_EXISTS };
            }

            var user = await _db.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            MapUser(obj, user);

            await _db.SaveChangesAsync();

            return user.GetDTO();
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

            if (obj.Email != null && Regex.IsMatch(obj.Email ?? "", @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+"))
            {
                user.Email = obj.Email;
            }

            if (obj.Password != null && Regex.IsMatch(obj.Password ?? "", GlobalConstants.PassRegex))
            {
                user.Password = obj.Password;
            }

            if (obj.PhoneNumber != null && Regex.IsMatch(obj.PhoneNumber ?? "", GlobalConstants.PhoneRegex))
            {
                user.PhoneNumber = obj.PhoneNumber;
            }
        }

        private bool IsValidUser(string username, string email, string password, string phoneNumber)
        {
            var validUsername = username.Length >= 2 && username.Length <= 20;
            var validEmail = Regex.IsMatch(email ?? "", @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+");
            var validPassword = Regex.IsMatch(password ?? "", GlobalConstants.PassRegex);
            var validPhone = Regex.IsMatch(phoneNumber ?? "", GlobalConstants.PhoneRegex);
            return validUsername && validEmail && validPassword && validPhone;

        }
    }
}
