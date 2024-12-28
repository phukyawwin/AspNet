using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatApp.Domain.DTOs
{
    public record AvailableUserDto(string UserId, string ConnectionId, string FullName, string Email);
}
