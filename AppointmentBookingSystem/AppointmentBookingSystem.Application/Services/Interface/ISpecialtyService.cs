using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Services.Interface
{
    public interface ISpecialtyService
    {
        IEnumerable<Specialty> GetAllSpecialtys();
        Specialty GetSpecialtyById(int id);
        void CreateSpecialty(Specialty specialty);
        void UpdateSpecialty(Specialty specialty);
        bool DeleteSpecialty(int id);

    }
}
