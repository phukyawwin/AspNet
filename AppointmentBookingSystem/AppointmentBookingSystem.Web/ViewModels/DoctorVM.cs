using AppointmentBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentBookingSystem.Web.ViewModels
{
    public class DoctorVM
    {
        public Doctor? Docter { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? SpecialtyList { get; set; }
    }
}
