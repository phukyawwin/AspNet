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
    public class Doctor : ITimestampedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        // Foreign key for Specialty
        [ForeignKey("SpecialtyDetails")]
        public int SpecialtyId { get; set; } // This is the foreign key property

        // Navigation property to Specialty
        [ValidateNever]
        public Specialty SpecialtyDetails { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Date when the record was created
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Date when the record was last updated
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
