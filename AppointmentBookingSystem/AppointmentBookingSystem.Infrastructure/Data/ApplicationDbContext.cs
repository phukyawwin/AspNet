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
               
            }
        );
        }
    }
}
