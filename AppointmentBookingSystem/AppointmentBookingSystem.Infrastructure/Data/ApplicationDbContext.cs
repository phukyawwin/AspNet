using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Specialty> Specialty { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Slot> Slot { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Specialty>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
