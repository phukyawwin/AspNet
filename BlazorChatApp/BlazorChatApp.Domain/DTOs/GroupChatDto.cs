
using System.ComponentModel.DataAnnotations;


namespace BlazorChatApp.Domain.DTOs
{
    public class GroupChatDto
    {
        public int Id { get; set; }
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? SenderName {  get; set; }
        [Required]
        public string? Message { get; set; }
        public DateTime DateTime { get; set; }= DateTime.Now;
    }
}
