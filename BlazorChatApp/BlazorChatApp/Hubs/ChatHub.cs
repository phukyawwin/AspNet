using BlazorChatApp.Domain.Entities;
using BlazorChatApp.Infrastructure.Repository;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChatApp.Hubs
{
    public class ChatHub(ChatRepository chatRepo):Hub
    {
        public async Task SendMessage(Chat chat)
        {
            await chatRepo.SaveChatAsync(chat);
            await Clients.All.SendAsync("ReceiveMessage", chat);
        }
    }
}
