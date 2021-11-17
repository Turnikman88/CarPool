using CarPool.Common;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

        public async Task<ResponseAuthDTO> Authenticate(RequestAuthDTO model)
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

        public async Task<ResponseAuthDTO> GetByEmail(string email)
        {
            var user =  await _db.ApplicationUsers
                .Include(x => x.ApplicationRole)
                .Include(x => x.Ban)
                .Where(x => x.Email == email)                
                .FirstOrDefaultAsync();

            var model = new ResponseAuthDTO
            {
                isBlocked = user.Ban?.BlockedOn == null ? false : true,

            };
            if (model.isBlocked)
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
