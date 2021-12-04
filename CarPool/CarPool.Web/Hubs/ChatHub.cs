using CarPool.Services.Data.Contracts;
using CarPool.Web.ViewModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarPool.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IApplicationUserService _user;

        public ChatHub(IApplicationUserService user)
        {
            _user = user;
        }

        public async Task Send(string message, string groupId)
        {
            var userEmail = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var username = await _user.GetUserByEmailOrIdAsync(userEmail);

            await Clients.Group(groupId).SendAsync("NewMessage",
                    new MessageViewModel
                    {
                        User = $"{username.FirstName} {username.LastName}",
                        Text = message,
                        Date = System.DateTime.UtcNow.ToString("dd/MMM/yy")
                    });
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

    }
}
