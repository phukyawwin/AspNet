using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentBookingSystem.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ISpecialtyService _specialtyService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(IBookingService bookingService, ISpecialtyService specialtyService, UserManager<ApplicationUser> userManager)
        {

            _bookingService = bookingService;
            _specialtyService = specialtyService;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var booking = _bookingService.GetAllBooking();
            return View(booking);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            BookingVM bookingVM = new()
            {
                Specialties = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                
                Customers = customers.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }).ToList()
            };
            
            return View(bookingVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BookingVM obj)
        {


            bool slotExists = _bookingService.CheckBookingExists(obj.Booking.Id);

            if (ModelState.IsValid && !slotExists)
            {
                _bookingService.CreateBooking(obj.Booking);
                TempData["success"] = "The Booking has been created successfully.";
                return RedirectToAction(nameof(Index));
            }

            if (slotExists)
            {
                TempData["error"] = "The Booking already exists.";
            }
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            obj.Specialties = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            obj.Customers = customers.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
            return View(obj);
        }
      
        public async Task<IActionResult> DeleteAsync(int bookingId)
        {
            Booking? objFromDb = _bookingService.GetBookingById(bookingId);
            if (objFromDb is not null)
            {
                _bookingService.DeleteBooking(objFromDb.Id);
                TempData["success"] = "The Booking has been cancle successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The booking could not be cancle.";
            return View();
            
        }

        
    }
}
