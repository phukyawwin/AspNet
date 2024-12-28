using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatApp.Domain.DTOs
{
    public class IndividualChatDto
    {
        public int Id { get; set; }
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? SenderName { get; set; }
        [Required]
        public string? ReceiverId { get; set; }
        [Required]
        public string? ReceiverName { get; set; }
        [Required]
        public string? Message { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
