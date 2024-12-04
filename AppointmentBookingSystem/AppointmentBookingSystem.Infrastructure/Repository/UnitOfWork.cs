using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Application.Common.Interfaces;
using AppointmentBookingSystem.Infrastructure.Data;

namespace AppointmentBookingSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISpecialtyRepository SpecialtyRepository {get; private set;}
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            SpecialtyRepository = new SpecialtyRepository(_db);
           
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
