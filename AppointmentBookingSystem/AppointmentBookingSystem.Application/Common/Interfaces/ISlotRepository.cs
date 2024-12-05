using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Common.Interfaces
{
    public interface ISlotRepository : IRepository<Slot>
    {
        void Update(Slot entity);
        void Delete(Slot entity);
    }
}
