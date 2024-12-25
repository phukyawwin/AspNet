using BlazorChatApp.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChatApp.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(Chat chat)
        => await Clients.All.SendAsync("ReceiveMessage", chat);
    }
}
