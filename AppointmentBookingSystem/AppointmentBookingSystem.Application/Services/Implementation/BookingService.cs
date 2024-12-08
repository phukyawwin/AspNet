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
    public class BookingService:IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CheckBookingExists(int Booking)
        {
            return _unitOfWork.DoctorRepository.Any(u => u.Id == Booking);
        }

        public void CreateBooking(Booking booking)
        {
            _unitOfWork.BookingRepository.Add(booking);
            _unitOfWork.Save();     
        }

        public bool DeleteBooking(int id)
        {
            try
            {
                Booking? objFromDb = _unitOfWork.BookingRepository.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    _unitOfWork.BookingRepository.Delete(objFromDb);
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
        public bool checkBookingExitByCustomer(Booking booking)
        {
            Booking existBooking=_unitOfWork.BookingRepository.Get(u => u.Status == "Confirmed" && u.SlotId == booking.SlotId &&
             u.CustomerId == booking.CustomerId && u.AppointmentDate==booking.AppointmentDate);
            return existBooking is not null;
        }
        public IEnumerable<Booking> GetAllBooking()
        {
            return _unitOfWork.BookingRepository.GetAll( includeProperties: "Customer,Slot.Doctor.SpecialtyDetails");
        }
        public IEnumerable<Booking> GetAllBookingByCustomer(string id)
        {
            return _unitOfWork.BookingRepository.GetAll(u => u.CustomerId == id, includeProperties: "Customer,Slot.Doctor.SpecialtyDetails");
        }
        public Booking GetBookingById(int id)
        {
            return _unitOfWork.BookingRepository.Get(u => u.Id == id, includeProperties: "Customer,Slot.Doctor.SpecialtyDetails");
        }

        public void UpdateBooking(Booking booking)
        {
            _unitOfWork.BookingRepository.Update(booking);
            _unitOfWork.Save();
        }

        public int getBookingCountOnDate(Booking booking)
        {
           return _unitOfWork.BookingRepository.GetAll(u => u.Status == "Confirmed" && u.SlotId == booking.SlotId &&
             u.AppointmentDate == booking.AppointmentDate).Count();
        }
    }
}
