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
    public class BanService : IBanService
    {
        private readonly CarPoolDBContext _db;

        public BanService(CarPoolDBContext db)
        {
            _db = db;
        }

        public async Task<BanDTO> BanUserAsync(string email, string reason, string days)
        {
            if (string.IsNullOrEmpty(reason) || string.IsNullOrWhiteSpace(reason))
                reason = GlobalConstants.NO_COMMENT;

            var user = await _db.ApplicationUsers
                .Include(x=>x.Ban)
               .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return new BanDTO() { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            user.Ban = new CarPool.Data.Models.DatabaseModels.Ban();
            user.Ban.BlockedOn = DateTime.UtcNow.Date;
            user.Ban.Reason = reason;

            if (!string.IsNullOrEmpty(days) || !string.IsNullOrWhiteSpace(days))
                user.Ban.BlockedDue = DateTime.UtcNow.AddDays(double.Parse(days)); 
            //if days are empty, null or whitespace ban will be permanent

            await _db.SaveChangesAsync();
            return new BanDTO { 
                ApplicationUserId = user.Id,
                UserEmail = user.Email,
                BlockedOn = user.Ban.BlockedOn,
                BlockedDue = user.Ban.BlockedDue,
                Reason = reason };
        }

        public async Task<IEnumerable<ApplicationUserDisplayDTO>> GetAllBannedUsersAsync(int page)
        {
            return await _db.ApplicationUsers
                .Include(x => x.Address)
                .Include(x => x.Ratings)
                .Include(x => x.Trips)
                .Include(x => x.Vehicle)
                .Include(x => x.Ban)
                .Where(x=>x.Ban.BlockedDue > DateTime.UtcNow)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDisplayDTO())
                .ToListAsync();
        }

        public async Task<BanDTO> UnbanUserAsync(string email)
        {
            var user = await _db.ApplicationUsers.Include(x=>x.Ban)
               .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return new BanDTO() { ErrorMessage = GlobalConstants.USER_NOT_FOUND };

            user.Ban.BlockedOn = null;
            user.Ban.BlockedDue = null;

            await _db.SaveChangesAsync();

            return new BanDTO() { ApplicationUserId = user.Id, BanRemovedMessage = string.Format(GlobalConstants.USER_UNBLOCKED, $"{user.Email}") };
        }
    }
}
