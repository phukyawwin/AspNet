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
    public class SlotService:ISlotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CheckSlotExists(int slotId)
        {
            return _unitOfWork.SlotRepository.Any(u => u.Id == slotId);
        }

        public void CreateSlot(Slot slot)
        {
            _unitOfWork.SlotRepository.Add(slot);
            _unitOfWork.Save();
        }

        public bool DeleteSlot(int id)
        {
            try
            {
                Slot? objFromDb = _unitOfWork.SlotRepository.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    _unitOfWork.SlotRepository.Delete(objFromDb);
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

        public IEnumerable<Slot> GetAllSlot()
        {
            return _unitOfWork.SlotRepository.GetAll(u => u.IsAvailable == true, includeProperties: "Doctor");
        }

        public Slot GetSlotById(int id)
        {
            return _unitOfWork.SlotRepository.Get(u => u.Id == id, includeProperties: "Doctor");
        }
        public IEnumerable<Slot> GetSlotByDoctorId(int doctorId)
        {
            return _unitOfWork.SlotRepository.GetAll(u => u.DoctorId == doctorId, includeProperties: "Doctor");
        }
        public void UpdateSlot(Slot slot)
        {
            _unitOfWork.SlotRepository.Update(slot);
            _unitOfWork.Save();
        }
    }
}
