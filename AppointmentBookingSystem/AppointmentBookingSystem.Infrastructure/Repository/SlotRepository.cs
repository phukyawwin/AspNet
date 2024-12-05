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
    public class SlotRepository: Repository<Slot>,ISlotRepository
    {
        private readonly ApplicationDbContext _db;
        public SlotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Delete(Slot entity)
        {
            entity.IsAvailable = false;
            Update(entity);

        }

        public void Update(Slot entity)
        {
            _db.Slot.Update(entity);
        }
    }
}
