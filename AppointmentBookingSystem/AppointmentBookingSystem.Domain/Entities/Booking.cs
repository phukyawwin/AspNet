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
    public class Booking : ITimestampedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; } 

        [ValidateNever]
        public ApplicationUser Customer { get; set; }

        [Required]
        [ForeignKey("Slot")]
        public int SlotId { get; set; }

        [ValidateNever]
        public Slot Slot { get; set; }

        [Required]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime AppointmentDate { get; set; }

        // Status of the booking (e.g., Pending, Confirmed,Done ,Cancelled)
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Confirmed";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
