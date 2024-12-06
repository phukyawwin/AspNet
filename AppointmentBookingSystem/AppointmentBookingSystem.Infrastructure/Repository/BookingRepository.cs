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
    public class BookingRepository: Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Delete(Booking entity)
        {
            entity.Status = "Cancle";
            Update(entity);

        }

        public void Update(Booking entity)
        {
            _db.Booking.Update(entity);
        }
    }
}
