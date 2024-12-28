using BlazorChatApp.Domain.DTOs;
using BlazorChatApp.Domain.Entities;
using BlazorChatApp.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ChatRepository chatRepo) : ControllerBase
    {
        [HttpGet("group-chats")]
        public async Task<IActionResult> GetGroupChatsAsync()=>Ok(await chatRepo.GetGroupChatsAsync());
        [HttpGet("users")]
        public async Task<IActionResult>GetUsersAsync()=>Ok(await chatRepo.GetAvailableUsersAsync());
        [HttpPost("individual")]
        public async Task<IActionResult> GetIndividualChatsAsync(RequestChatDto requestChatDto) =>
            Ok(await chatRepo.GetIndividualChatsAsync(requestChatDto));


    }
}
