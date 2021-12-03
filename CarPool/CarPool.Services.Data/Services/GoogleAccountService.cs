using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class GoogleAccountService : IGoogleAccountService
    {
        private readonly CarPoolDBContext _db;

        public GoogleAccountService(CarPoolDBContext db)
        {
            _db = db;
        }

        public async Task AddGoogleAccount(string email)
        {
            await _db.GoogleAccount.AddAsync(new GoogleAccount { Email = email });
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsGoogleAccount(string email)
        {
            return await _db.GoogleAccount.AnyAsync(x => x.Email == email);
        }

        public async Task DeleteGoogleAccount(string email)
        {
            var user = await _db.GoogleAccount.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                _db.GoogleAccount.Remove(user);
                await _db.SaveChangesAsync();
            }
        }
    }
}
