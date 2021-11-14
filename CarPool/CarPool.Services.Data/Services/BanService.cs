using CarPool.Data;
using CarPool.Services.Data.Contracts;
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
            this._db = db;
        }

        public async Task RemoveBanAsync()
        {
            await _db.ApplicationUsers.Include(x => x.Ban)
                .Where(x => x.Ban.BlockedDue < DateTime.UtcNow.Date)
                .ForEachAsync(x => { x.Ban.BlockedOn = null; x.Ban.BlockedDue = null; });

            await _db.SaveChangesAsync();
        }
    }
}
