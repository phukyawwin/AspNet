using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatApp.Domain.Entities
{
    public class AvailableUser
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ConnectionId { get; set; } = string.Empty;
    }
}
