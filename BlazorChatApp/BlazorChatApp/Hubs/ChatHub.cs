
using BlazorChatApp.Domain.DTOs;
using BlazorChatApp.Domain.Entities;
using BlazorChatApp.Infrastructure.Repository;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChatApp.Hubs
{
    public class ChatHub(ChatRepository chatRepo):Hub
    {
        public async Task SendMessageToGroup(GroupChat chat)
        {
            var saveChatDTO=await chatRepo.AddChatToGroupAsync(chat);
            await Clients.All.SendAsync("ReceiveGroupMessages", saveChatDTO);
        }
        public async Task AddAvailableUser(AvailableUser availableUser)
        {
            availableUser.ConnectionId=Context.ConnectionId;
            var availableUsers= await chatRepo.AddAvailableUserAsync(availableUser);
            await Clients.All.SendAsync("NotifyAllClient", availableUsers);
        } 
        public async Task RemoveUserAsync(string userId)
        {
            var availableUsers = await chatRepo.RemoveUserAsync(userId);
            await Clients.All.SendAsync("NotifyAllClient", availableUsers);
        }
        public async Task AddIndividualChat(IndividualChat individualChat)
        {
            await chatRepo.AddIndividualChatAsync(individualChat);
            var requestDTO = new RequestChatDto()
            {
                ReceiverId = individualChat.ReceiverId,
                SenderId = individualChat.SenderId,

            };
            var getChats=await chatRepo.GetIndividualChatsAsync(requestDTO);
            var prepareIndividualChat = new IndividualChatDto()
            {
                SenderId=individualChat.SenderId,
                ReceiverId=individualChat.ReceiverId,
                Message = individualChat.Message,
                DateTime =individualChat.Date,
                ReceiverName=getChats.Where(_=>_.ReceiverId==individualChat.ReceiverId).FirstOrDefault()!.ReceiverName,
                SenderName = getChats.Where(_ => _.SenderId == individualChat.SenderId).FirstOrDefault()!.SenderName,
            };
            await Clients.User(individualChat.ReceiverId!).SendAsync("ReceiveIndividualMessage",prepareIndividualChat);
        }
       
    }
}
