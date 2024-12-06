using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Application.Common.Interfaces;
using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Services.Implementation
{
    public class DoctorService: IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CheckDoctorExists(int Doctor_Number)
        {
            return _unitOfWork.DoctorRepository.Any(u => u.Id == Doctor_Number);
        }

        public void CreateDoctor(Doctor doctor)
        {
            _unitOfWork.DoctorRepository.Add(doctor);
            _unitOfWork.Save();
        }

        public bool DeleteDoctor(int id)
        {
            try
            {
                Doctor? objFromDb = _unitOfWork.DoctorRepository.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    _unitOfWork.DoctorRepository.Delete(objFromDb);
                    _unitOfWork.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _unitOfWork.DoctorRepository.GetAll(u => u.IsActive == true,includeProperties: "SpecialtyDetails");
        }

        public Doctor GetDoctorById(int id)
        {
            return _unitOfWork.DoctorRepository.Get(u => u.Id == id, includeProperties: "SpecialtyDetails");
        }

        public IEnumerable<Doctor> GetDoctorBySpecialtyId(int SpecialtyId)
        {
            return _unitOfWork.DoctorRepository.GetAll(u => u.SpecialtyId == SpecialtyId, includeProperties: "SpecialtyDetails");
        }

        public void UpdateDoctor(Doctor doctor)
        {
            _unitOfWork.DoctorRepository.Update(doctor);
            _unitOfWork.Save();
        }
    }
}
