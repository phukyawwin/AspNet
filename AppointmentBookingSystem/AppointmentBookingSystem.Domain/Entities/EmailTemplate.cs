using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Domain.Entities
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; } // Template name
        public string Subject { get; set; } // Email subject
        public string Body { get; set; } // Email body content
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Timestamp for creation
      
    }
}
