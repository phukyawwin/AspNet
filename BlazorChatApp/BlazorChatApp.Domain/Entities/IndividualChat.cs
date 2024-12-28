using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatApp.Domain.Entities
{
    public class IndividualChat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? ReceiverId { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
