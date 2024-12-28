
using BlazorChatApp.Domain.DTOs;
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
        public async Task AddAvailableUser(AvailableUser availableUser)
        {
            availableUser.ConnectionId=Context.ConnectionId;
            await chatRepo.AddAvailableUserAsync(availableUser);
            await Clients.All.SendAsync("NotifyAllClient", await GetUsers());
        } 
        public async Task RemoveUserAsync(string userId)
        {
            await chatRepo.RemoveUserAsync(userId);
            await Clients.All.SendAsync("NotifyAllClient",await GetUsers());
        }
        private async Task<List<AvailableUserDto>> GetUsers()=> await chatRepo.GetAvailableUsersAsync();
    }
}
