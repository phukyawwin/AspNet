using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ISpecialtyRepository SpecialtyRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        ISlotRepository SlotRepository { get; }
        IBookingRepository BookingRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        void Save();
    }
}
