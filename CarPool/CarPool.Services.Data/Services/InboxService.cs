using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class InboxService : IInboxService
    {
        private readonly CarPoolDBContext _db;
        private readonly IApplicationUserService _ap;

        public InboxService(CarPoolDBContext db, IApplicationUserService ap)
        {
            _db = db;
            _ap = ap;
        }

        public async Task SendMessageAsync(string sender, string reciever, string message)
        {
            var senderUser = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == sender || x.Id.ToString() == sender);
            var recieverUser = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == reciever || x.Id.ToString() == reciever);

            if (senderUser != null && recieverUser != null)
            {
                await _db.Inboxes.AddAsync(new Inbox { FromUserId = senderUser.Id, ApplicationUserId = recieverUser.Id, Message = message });
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InboxDTO>> GetUserMessages(string userIdOrEmail)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == userIdOrEmail || x.Id.ToString() == userIdOrEmail);
            var messages = await _db.Inboxes.Where(x => x.ApplicationUserId == user.Id).Select(x => x.GetDTO()).ToListAsync();
            await _db.Inboxes.Where(x => x.ApplicationUserId == user.Id).ForEachAsync(x => x.Seen = true);
            await _db.SaveChangesAsync();

            for (int i = 0; i < messages.Count; i++)
            {
                var author = await _ap.GetUserByEmailOrIdAsync(messages[0].Author);
                messages[i].Author = $"{author.FirstName} {author.LastName}";
            }
            return messages.OrderByDescending(x => x.SendOnDate);
        }

        public async Task<bool> HasUnreadMessages(string userIdOrEmail)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == userIdOrEmail || x.Id.ToString() == userIdOrEmail);
            return await _db.Inboxes.AnyAsync(x => x.ApplicationUserId == user.Id && x.Seen == false);
        }
    }
}
