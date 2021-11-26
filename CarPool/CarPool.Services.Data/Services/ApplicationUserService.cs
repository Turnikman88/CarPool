using CarPool.Common;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task<ApplicationUserDTO> DeleteAsync(string email)
        {
            var user = await _db.ApplicationUsers
                                .Include(x => x.Ban)
                                .Include(x => x.ApplicationRole)
                                .FirstOrDefaultAsync(x => x.Email == email);

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
            return await _db.ApplicationUsers.Include(x => x.Address)
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
            var user = await _db.ApplicationUsers.Include(x => x.Address)
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
            var errorMessage = await CheckUserData(obj);
            if (errorMessage != null)
            {
                return new ApplicationUserDTO { ErrorMessage = errorMessage };
            }

            var newUser = obj.GetEntity();

            if (!IsValidUser(obj.Username, obj.Email, obj.Password, obj.PhoneNumber))
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            await _db.ApplicationUsers.AddAsync(newUser);
            await _db.SaveChangesAsync();

            await _db.ProfilePictures.AddAsync(new ProfilePicture
            {
                ImageLink = GlobalConstants.DefaultPicture,
                ApplicationUserId = newUser.Id
            });

            await _db.SaveChangesAsync();

            return await _db.ApplicationUsers.Include(x => x.ApplicationRole)
                                             .Where(x => x.Id == newUser.Id)
                                             .Select(x => x.GetDTO())
                                             .FirstOrDefaultAsync();
        }

        private async Task<string> CheckUserData(ApplicationUserDTO obj)
        {
            if (obj is null || obj.Username is null || obj.FirstName is null
                || obj.LastName is null || obj.Email is null
                || !Regex.IsMatch(obj.PhoneNumber ?? "", GlobalConstants.PhoneRegex)
                || obj.Password is null)
            {
                return GlobalConstants.INCORRECT_DATA;
            }

            if (await _db.ApplicationUsers.AnyAsync(x => x.Email == obj.Email))
            {
                return GlobalConstants.USER_EXISTS;
            }

            if (await _db.ApplicationUsers.AnyAsync(x => x.PhoneNumber == obj.PhoneNumber))
            {
                return GlobalConstants.USER_PHONE_EXISTS;
            }

            if (!await _db.Addresses.AnyAsync(x => x.Id == obj.AddressId))
            {
                return GlobalConstants.ADDRESS_NOT_FOUND;
            }

            return null;
        }

        public async Task<ApplicationUserDTO> UpdateAsync(string email, ApplicationUserDTO obj)
        {
            if (await _db.ApplicationUsers.Where(x => x.Email != email).FirstOrDefaultAsync(x => x.Email == obj.Email) != null)
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_EXISTS };
            }

            var user = await _db.ApplicationUsers.Include(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            MapUser(obj, user);

            await _db.SaveChangesAsync();

            return user.GetDTO();
        }

        public async Task<ApplicationUserDTO> UpdatePasswordAsync(string email, string newPassword)
        {
            var user = await _db.ApplicationUsers.Include(x => x.ApplicationRole).FirstOrDefaultAsync(x => x.Email == email);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _db.SaveChangesAsync();

            return user.GetDTO();
        }


        public async Task<List<TopUserDTO>> TopUsersAsync()
        {
            return await _db.ApplicationUsers.Include(x => x.Ratings)
                                             .Include(x => x.Trips)
                                             .Include(x => x.ProfilePicture)
                                             .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
                                                .ThenByDescending(x => x.Trips.Count)
                                             .Take(10)
                                             .Select(x => x.GetTopUserDTO())
                                             .ToListAsync();
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
                user.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            }

            if (obj.PhoneNumber != null && Regex.IsMatch(obj.PhoneNumber ?? "", GlobalConstants.PhoneRegex))
            {
                user.PhoneNumber = obj.PhoneNumber;
            }

            user.AddressId = obj.AddressId;
        }

        private bool IsValidUser(string username, string email, string password, string phoneNumber)
        {
            var validUsername = username.Length >= 2 && username.Length <= 20;
            var validEmail = Regex.IsMatch(email ?? "", @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+");
            var validPassword = Regex.IsMatch(password ?? "", GlobalConstants.PassRegex);
            var validPhone = Regex.IsMatch(phoneNumber ?? "", GlobalConstants.PhoneRegex);
            return validUsername && validEmail && validPassword && validPhone;

        }

        public async Task<int> UsersCountAsync()
        {
            return await _db.ApplicationUsers.CountAsync();
        }
    }
}
