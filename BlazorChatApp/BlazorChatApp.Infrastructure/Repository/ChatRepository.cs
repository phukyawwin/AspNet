using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorChatApp.Client.DTO;
using BlazorChatApp.Domain.Entities;
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
            _userManager= userManager;
        }
        public async Task SaveChatAsync(Chat chat)
        {
            _db.Chats.Add(chat);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Chat>> GetChatsAsync() =>
            await _db.Chats.ToListAsync();

        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            var getUser = await _db.AvailableUsers.FirstOrDefaultAsync(
                _ => _.UserId == availableUser.UserId);
            if (getUser != null)
                getUser.ConnectionId = availableUser.ConnectionId;
            else
                _db.AvailableUsers.Add(availableUser);

            await _db.SaveChangesAsync();
        }
        public async Task<List<AvailableUserDto>> GetAvailableUsersAsync()
        {
            var list=new List<AvailableUserDto>();
            var user=await _db.AvailableUsers.ToListAsync();
            foreach (var u in user) {
                list.Add(new AvailableUserDto ( 
                    UserId : u.UserId,
                    ConnectionId:u.ConnectionId,
                    FullName:(await _userManager.FindByIdAsync(u.UserId!)!)!.FullName,
                    Email: (await _userManager.FindByIdAsync(u.UserId!)!)!.Email!
                    ));
            }

            return list;
        }
        public async Task RemoveUserAsync(string userId)
        {
            var user =await _db.AvailableUsers.FirstOrDefaultAsync(u=>u.UserId==userId);
            if(user != null)
            {
                _db.AvailableUsers.Remove(user);
                await _db.SaveChangesAsync();
            }
        }
    }
}
