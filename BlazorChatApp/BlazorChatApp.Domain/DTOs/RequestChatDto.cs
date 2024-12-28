using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatApp.Domain.DTOs
{
    public class RequestChatDto
    {
        public string? SenderId {  get; set; }
        public string? ReceiverId { get; set; }
    }
}
