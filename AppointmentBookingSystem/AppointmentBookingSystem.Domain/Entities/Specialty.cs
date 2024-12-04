using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Domain.Entities
{
    public class Specialty
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
