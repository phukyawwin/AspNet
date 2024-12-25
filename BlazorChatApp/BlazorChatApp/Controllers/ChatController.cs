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
        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetChatsAsync()
        =>Ok(await chatRepo.GetChatsAsync());
    }
}
