using CarPool.Services.Mapping.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IInboxService
    {
        Task SendMessageAsync(string sender, string reciever, string message);
        Task<bool> HasUnreadMessages(string userIdOrEmail);
        Task<IEnumerable<InboxDTO>> GetUserMessages(string userId);
    }
}
