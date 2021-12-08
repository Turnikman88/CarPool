using CarPool.Common;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<BanDTO> BanUserAsync(string email, string reason, DateTime? days)
        {
            if (string.IsNullOrEmpty(reason) || string.IsNullOrWhiteSpace(reason))
                reason = GlobalConstants.NO_COMMENT;

            var user = await _db.ApplicationUsers
                .Include(x => x.Ban)
               .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return new BanDTO() { ErrorMessage = GlobalConstants.USER_NOT_FOUND };

            user.Ban = new Ban();
            user.Ban.BlockedOn = DateTime.UtcNow.Date;
            user.Ban.Reason = reason;
            user.ApplicationRoleId = 3;

            var report = await _db.Ratings
                .Include(x => x.ApplicationUser)
                .Where(x => x.ApplicationUser.Email == email)
                .FirstOrDefaultAsync();

            if (report != null)
            {
                report.IsReport = false;
            }

            if (days != null)
            {
                user.Ban.BlockedDue = days;
            }
            //if days are empty, null or whitespace ban will be permanent

            await _db.SaveChangesAsync();
            return new BanDTO
            {
                Id = user.Ban.Id,
                ApplicationUserId = user.Id,
                UserEmail = user.Email,
                BlockedOn = user.Ban.BlockedOn,
                BlockedDue = user.Ban.BlockedDue,
                Reason = reason
            };
        }

        public async Task<IEnumerable<BanDTO>> GetAllBannedUsersAsync(int page)
        {
            return await _db.ApplicationUsers
                .Include(x => x.Ban)
                .Include(x => x.ProfilePicture)
                .Where(x => x.ApplicationRoleId == 3)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetBanDTO())
                .ToListAsync();
        }

        public async Task<IEnumerable<ReportedDTO>> GetTopReportedUsersAsync()
        {

            return await _db.ApplicationUsers
                .Include(x => x.ProfilePicture)
                .Include(x => x.Ratings)
                .Where(x => x.Ratings.Any(x => x.IsReport == true))
                .Select(x => new ReportedDTO
                {
                    Email = x.Email,
                    Picture = x.ProfilePicture.ImageLink,
                    Reason = string.Join(", ", x.Ratings
                        .Where(x => x.IsReport == true && x.Feedback != GlobalConstants.NO_FEEDBACK)
                        .Select(x => x.Feedback))
                })
                .ToListAsync();
        }

        public async Task<ReportedDTO> GetReportedUserByEmailAsync(string email)
        {
            var reported = await _db.ApplicationUsers
                .Include(x => x.ProfilePicture)
                .Include(x => x.Ratings)
                .Where(x => x.Ratings.Any(x => x.IsReport == true) && x.Email == email)
                .Select(x => new ReportedDTO
                {
                    Email = x.Email,
                    Picture = x.ProfilePicture.ImageLink,
                    Reason = string.Join(", ", x.Ratings
                        .Where(x => x.IsReport == true && x.Feedback != GlobalConstants.NO_FEEDBACK)
                        .Select(x => x.Feedback))
                })
                .FirstOrDefaultAsync();

            if (reported is null)
            {
                return new ReportedDTO { Message = GlobalConstants.USER_NOT_FOUND };
            }

            return reported;
        }

        public async Task IgnoreReportAsync(string email)
        {
            await _db.Ratings
                .Where(x => x.ApplicationUser.Email == email)
                .ForEachAsync(x => x.IsReport = false);
            await _db.SaveChangesAsync();
        }

        public async Task<BanDTO> UnbanUserAsync(string email)
        {
            var user = await _db.ApplicationUsers.Include(x => x.Ban)
               .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return new BanDTO() { ErrorMessage = GlobalConstants.USER_NOT_FOUND };

            user.Ban.BlockedOn = null;
            user.Ban.BlockedDue = null;
            user.ApplicationRoleId = 2;

            await _db.SaveChangesAsync();

            return new BanDTO() { ApplicationUserId = user.Id, BanRemovedMessage = string.Format(GlobalConstants.USER_UNBLOCKED, $"{user.Email}") };
        }

        public async Task<int> GetMaxPageAsync()
        {
            var count = await this._db.Bans.CountAsync();
            var page = count / GlobalConstants.PageSkip;
            return page;
        }
    }
}
