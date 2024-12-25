using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorChatApp.Domain.Entities;
using BlazorChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorChatApp.Infrastructure.Repository
{
    public class ChatRepository
    {
        private readonly ApplicationDbContext _db;
        public ChatRepository(ApplicationDbContext db) 
        {
            _db = db;
        }
        public async Task SaveChatAsync(Chat chat)
        {
            _db.Chats.Add(chat);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Chat>> GetChatsAsync()=>
            await _db.Chats.ToListAsync();
    }
}
