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

        public async Task<IActionResult> UpdateAsync(int bookingId)
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
                }).ToList(),
                Booking = _bookingService.GetBookingById(bookingId)
            };
            if (bookingVM.Booking == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(bookingVM);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAsync(BookingVM bookingVM)
        {
            
            if (ModelState.IsValid)
            {
                _bookingService.UpdateBooking(bookingVM.Booking);
                TempData["success"] = "The Booking has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            bookingVM.Specialties = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            bookingVM.Customers = customers.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
            return View(bookingVM);
        }
        public async Task<IActionResult> DeleteAsync(int bookingId)
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
                }).ToList(),
                Booking = _bookingService.GetBookingById(bookingId)
            };
            if (bookingVM.Booking == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(bookingVM);
        }



        [HttpPost]
        public IActionResult Delete(BookingVM bookingVM)
        {
            Booking? objFromDb = _bookingService.GetBookingById(bookingVM.Booking.Id);
            if (objFromDb is not null)
            {
                _bookingService.DeleteBooking(objFromDb.Id);
                TempData["success"] = "The Booking has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The booking could not be deleted.";
            return View();
        }

        
    }
}
