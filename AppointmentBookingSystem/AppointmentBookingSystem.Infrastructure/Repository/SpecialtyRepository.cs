using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Application.Common.Interfaces;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Infrastructure.Data;

namespace AppointmentBookingSystem.Infrastructure.Repository
{
    public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
    {
        private readonly ApplicationDbContext _db;

        public SpecialtyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Delete(Specialty entity)
        {
            entity.IsActive = false;
            Update(entity);

        }

        public void Update(Specialty entity)
        {
            _db.Specialties.Update(entity);
        }
    }
}
