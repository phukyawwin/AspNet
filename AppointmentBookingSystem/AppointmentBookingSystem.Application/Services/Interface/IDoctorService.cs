using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Services.Interface
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);
        void CreateDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(int id);
        IEnumerable<Doctor> GetDoctorBySpecialtyId(int SpecialtyId);
        bool CheckDoctorExists(int doctor_Number);
    }
}
