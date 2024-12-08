using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AppointmentBookingSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        public DbSet<Specialty> Specialty { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Slot> Slot { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Specialty>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<EmailTemplate>().HasData(
            new EmailTemplate
            {
                Id = 1,
                Name = "Appointment Confirmation",
                Subject = "Appointment Confirmation",
                Body = "Dear {{CustomerName}},<br>Your appointment is confirmed for {{AppointmentDate}} at {{SlotStartTime}} - {{SlotEndTime}} with Dr. {{DoctorName}}.",
               
            },
             new EmailTemplate
             {
                 Id = 2,
                 Name = "Booking Cancellation",
                 Subject = "Booking Cancellation Notification",
                 Body = "Dear {{CustomerName}},<br>Your appointment on {{AppointmentDate}} at {{SlotStartTime}} - {{SlotEndTime}} with Dr. {{DoctorName}} has been cancelled. We apologize for any inconvenience."
             },
              new EmailTemplate
              {
                  Id = 3,
                  Name = "Appointment Reminder",
                  Subject = "Reminder: Your Appointment with Dr. {{DoctorName}}",
                  Body = "Dear {{CustomerName}},<br>This is a reminder that you have an appointment scheduled for {{AppointmentDate}} at {{SlotStartTime}} - {{SlotEndTime}} with Dr. {{DoctorName}}.<br>We look forward to seeing you."
              }
        );
        }
    }
}
