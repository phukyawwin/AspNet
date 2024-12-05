using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AppointmentBookingSystem.Domain.Entities
{
    public class Slot : ITimestampedEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Doctor Name")]
        // Foreign key for Doctor
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        // Navigation property to Doctor
        [ValidateNever]
        public Doctor Doctor { get; set; }

        // Day of the week (e.g., Monday, Tuesday, etc.)
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        // Start time of the slot
        [Required]
        public TimeSpan StartTime { get; set; }

        // End time of the slot
        [Required]
        public TimeSpan EndTime { get; set; }

        // Indicates if the slot is available for booking
        [Required]
        public bool IsAvailable { get; set; } = true;

        // Maximum number of patients that can be booked for this slot
        [Required]
        public int MaxPatients { get; set; } = 100; // Default value can be set to 100 or any other number
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Date when the record was last updated
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
