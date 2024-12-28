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

        [HttpGet("users")]
        public async Task<ActionResult<List<Chat>>> GetAvailableUsersAsync()
       => Ok(await chatRepo.GetAvailableUsersAsync());

        [HttpPost("removeUser")]
        public async Task<IActionResult> RemoveUser([FromBody] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required");
            }

            await chatRepo.RemoveUserAsync(userId);
            return Ok();
        }
    }
}
