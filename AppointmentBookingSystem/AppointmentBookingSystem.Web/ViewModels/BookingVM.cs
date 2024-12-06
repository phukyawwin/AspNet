using AppointmentBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentBookingSystem.Web.ViewModels
{
    public class BookingVM
    {
        public Booking? Booking { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Customers { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Specialties { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Doctors { get; set; }
        [ValidateNever]
        public int SpecialtyId { get; set; }
        [ValidateNever]
        public int DoctorId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SlotList { get; set; }


    }
}
