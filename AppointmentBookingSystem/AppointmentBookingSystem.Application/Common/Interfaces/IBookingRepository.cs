using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Common.Interfaces
{
    public interface IBookingRepository:IRepository<Booking>
    {
        void Update(Booking entity);
        void Delete(Booking entity);
    }
}
