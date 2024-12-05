using AppointmentBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentBookingSystem.Web.ViewModels
{
    public class SlotVM
    {
        public Slot? Slot { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? DoctorList { get; set; }
    }
}
