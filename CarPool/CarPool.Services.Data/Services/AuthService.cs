﻿using CarPool.Common;
using CarPool.Data;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly CarPoolDBContext _db;

        public AuthService(CarPoolDBContext db)
        {
            _db = db;
        }

        public async Task<bool> IsExistingAsync(string email)
        {
            return await _db.ApplicationUsers.AnyAsync(x => x.Email == email);
        }
        public async Task<ResponseAuthDTO> AuthenticateAsync(RequestAuthDTO model)
        {
            var user = await _db.ApplicationUsers
                .Include(x => x.ApplicationRole)
                .Include(x => x.Ban)
                .Where(x => x.Email == model.Email)
                .FirstOrDefaultAsync();

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return new ResponseAuthDTO { Message = GlobalConstants.WRONG_CREDENTIALS };
            }

            var token = generateJwtToken(user.GetDTO());

            return new ResponseAuthDTO
            {
                Token = token,
                Message = GlobalConstants.LOGGED,
            };
        }

        public async Task<ResponseAuthDTO> GetByEmailAsync(string email)
        {
            var user = await _db.ApplicationUsers
                .Include(x => x.ApplicationRole)
                .Include(x => x.Ban)
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();

            if (user is null)
                return new ResponseAuthDTO { Message = GlobalConstants.WRONG_CREDENTIALS };

            var model = new ResponseAuthDTO
            {
                isBlocked = user.Ban?.BlockedOn == null ? false : true,

            };

            if (model.isBlocked == true)
            {
                model.BlockedDue = user.Ban?.BlockedDue == null ? "Unknown" : user.Ban?.BlockedDue.ToString();
                model.Message = GlobalConstants.TRIP_USER_BLOCKED_JOIN;
            }
            else
            {
                model.Role = user.ApplicationRole.Name;
                model.Email = user.Email;
            }

            return model;
        }

        public string CheckConfirmTokenAndExtractEmail(string token)
        {
            var tokenByte = Convert.FromBase64String(token);
            var tokenToString = System.Text.Encoding.Unicode.GetString(tokenByte).Split("***");
            var email = tokenToString[0];
            var validUntil = DateTime.Parse(tokenToString[1]);

            if (DateTime.UtcNow < validUntil)
            {
                return email;
            }
            return "Token Expired";
        }

        public async Task<string> ConfirmEmail(string token)
        {
            var tokenByte = Convert.FromBase64String(token);
            var tokenToEmail = System.Text.Encoding.Unicode.GetString(tokenByte);
            var user = await _db.ApplicationUsers
                                .FirstOrDefaultAsync(x => x.Email == tokenToEmail);
            if (user != null && user.ApplicationRoleId == 4)
            {
                user.ApplicationRoleId = 2;
                user.EmailConfirmed = true;
                await _db.SaveChangesAsync();
                return user.Email;
            }
            return null;
        }

        public async Task<bool> IsEmailValidForPasswordReset(string email)
        {
            var user = await _db.ApplicationUsers
                                .FirstOrDefaultAsync(x => x.Email == email);
            if (user != null && user.ApplicationRoleId == 2 && user.EmailConfirmed == true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsPasswordValidAsync(string email, string password)
        {
            var userPassword = await _db.ApplicationUsers
                                        .Where(x => x.Email == email)
                                        .Select(x => x.Password)
                                        .FirstOrDefaultAsync();
            if (userPassword != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, userPassword);
            }
            return false;
        }


        private string generateJwtToken(ApplicationUserDTO user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GlobalConstants.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("role", user.Role),
                    new Claim("email", user.Email)}),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
