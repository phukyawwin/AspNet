using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Application.Common.Interfaces;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISpecialtyRepository SpecialtyRepository {get; private set;}
        public IDoctorRepository DoctorRepository { get; private set; }
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            SpecialtyRepository = new SpecialtyRepository(_db);
            DoctorRepository = new DoctorRepository(_db);

        }

        public void Save()
        {
            var entries = _db.ChangeTracker.Entries()
           .Where(e => e.Entity is ITimestampedEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var timestampedEntity = (ITimestampedEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    timestampedEntity.CreatedDate = DateTime.UtcNow; // Set CreatedDate for new entries
                }

                timestampedEntity.UpdatedDate = DateTime.UtcNow; // Always update UpdatedDate
            }
            _db.SaveChanges();
        }
    }
}
