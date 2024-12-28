using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorChatApp.Domain.Entities;
using BlazorChatApp.Domain.DTOs;
using BlazorChatApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorChatApp.Infrastructure.Repository
{
    public class ChatRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public ChatRepository(ApplicationDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<GroupChatDto> AddChatToGroupAsync(GroupChat chat)
        {
            var entity = _db.GroupChats.Add(chat).Entity;
            await _db.SaveChangesAsync();
            return new GroupChatDto()
            {
                SenderId = entity.SenderId,
                SenderName = (await _userManager.FindByIdAsync(entity.SenderId!)!)!.FullName,
                DateTime = entity.Date,
                Id = entity.Id,
                Message = entity.Message
            };
        }
        public async Task<List<GroupChatDto>> GetGroupChatsAsync()
        {
            var List = new List<GroupChatDto>();
            var chats = await _db.GroupChats.ToListAsync();
            foreach (var chat in chats)
            {
                List.Add(new GroupChatDto()
                {
                    SenderId = chat.SenderId,
                    SenderName = (await _userManager.FindByIdAsync(chat.SenderId!)!)!.FullName,
                    DateTime = chat.Date,
                    Id = chat.Id,
                    Message = chat.Message
                });

            }
            return List;
        }

        public async Task<List<AvailableUserDto>> AddAvailableUserAsync(AvailableUser availableUser)
        {
            var list = new List<AvailableUserDto>();
            var getUser = await _db.AvailableUsers.FirstOrDefaultAsync(_ => _.UserId == availableUser.UserId);
            if (getUser != null)
                getUser.ConnectionId = availableUser.ConnectionId;
            else
                _db.AvailableUsers.Add(availableUser);
            await _db.SaveChangesAsync();
            
            return await GetAvailableUsersAsync(); 
        }
        public async Task<List<AvailableUserDto>> GetAvailableUsersAsync()
        {
            var list = new List<AvailableUserDto>();
            var user = await _db.AvailableUsers.ToListAsync();
            foreach (var u in user)
            {
                list.Add(new AvailableUserDto()
                {
                    UserId = u.UserId,
                    FullName = (await _userManager.FindByIdAsync(u.UserId!)!)!.FullName
                });
            }

            return list;
        }
        public async Task<List<AvailableUserDto>> RemoveUserAsync(string userId)
        {
            var user = await _db.AvailableUsers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                _db.AvailableUsers.Remove(user);
                await _db.SaveChangesAsync();
            }
            return await GetAvailableUsersAsync();
        }
        public async Task AddIndividualChatAsync(IndividualChat individualChat)
        {
            _db.IndividualChats.Add(individualChat);
            await _db.SaveChangesAsync();
        }
        public async Task<List<IndividualChatDto>> GetIndividualChatsAsync(RequestChatDto requestChatDto)
        {
            var ChatList = new List<IndividualChatDto>();
            var chats = await _db.IndividualChats
                .Where(s => (s.SenderId == requestChatDto.SenderId && s.ReceiverId == requestChatDto.ReceiverId)
                || (s.SenderId == requestChatDto.ReceiverId && s.ReceiverId == requestChatDto.SenderId)).ToListAsync();

            if (chats != null)
            {
                foreach (var chat in chats)
                {
                    ChatList.Add(new IndividualChatDto()
                    {
                        SenderId = chat.SenderId,
                        ReceiverId = chat.ReceiverId,
                        SenderName= (await _userManager.FindByIdAsync(chat.SenderId!)!)!.FullName,
                        ReceiverName = (await _userManager.FindByIdAsync(chat.ReceiverId!)!)!.FullName,
                        Message = chat.Message,
                        DateTime = chat.Date,

                    });
                }
                return ChatList;
            }
            else
            {
                return null;
            }
           
        }

    }
}
