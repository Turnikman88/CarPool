using CarPool.Services.Data.Contracts;
using CarPool.Web.ViewModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
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

        public Task SendMessageToAll(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToUser(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task SendMessageToGroup(string group, string message)
        {
            var userEmail = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var username = _user.GetUserByEmailOrIdAsync(userEmail);

            return Clients.Group(group).SendAsync("ReceiveMessage", new MessageViewModel
            {
                User = $"{username.Result.FirstName} {username.Result.LastName}",
                Text = message,
                Date = System.DateTime.UtcNow.ToString("dd/MMM/yy")
            });
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }

    }
}
