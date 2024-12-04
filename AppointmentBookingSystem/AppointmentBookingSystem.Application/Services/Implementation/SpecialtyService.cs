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
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IUnitOfWork _unitOfWork;
     

        public SpecialtyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        public void CreateSpecialty(Specialty specialty)
        {
            _unitOfWork.SpecialtyRepository.Add(specialty);
            _unitOfWork.Save();
        }

        public bool DeleteSpecialty(int id)
        {
            Specialty? objFromDb = _unitOfWork.SpecialtyRepository.Get(u => u.Id == id);
            if (objFromDb is not null)
            {
                _unitOfWork.SpecialtyRepository.Delete(objFromDb);
                _unitOfWork.Save();
            }
            
            return true;
        }
        public IEnumerable<Specialty> GetAllSpecialtys()
        {
            return _unitOfWork.SpecialtyRepository.GetAll(u => u.IsActive == true);
        }

        public Specialty GetSpecialtyById(int id)
        {
            return _unitOfWork.SpecialtyRepository.Get(u => u.Id == id);
        }

        public void UpdateSpecialty(Specialty specialty)
        {
            _unitOfWork.SpecialtyRepository.Update(specialty);
            _unitOfWork.Save();
        }
    }
}
