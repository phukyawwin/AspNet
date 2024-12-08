using AppointmentBookingSystem.Application.Common.Contract;
using AppointmentBookingSystem.Application.Common.Utility;
using AppointmentBookingSystem.Application.Services.Implementation;
using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Web.ViewModels;
using Hangfire;
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
        private readonly ISlotService _slotService;
        private readonly IEmailService _emailService;

        public BookingController(IBookingService bookingService, ISpecialtyService specialtyService, UserManager<ApplicationUser> userManager, ISlotService slotService, IEmailService emailService)
        {

            _bookingService = bookingService;
            _specialtyService = specialtyService;
            _userManager = userManager;
            _slotService = slotService;
            _emailService = emailService;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            
            if (await _userManager.IsInRoleAsync(user, SD.Role_Admin))
            {
                var booking = _bookingService.GetAllBooking();
                return View(booking);
            }
            else
            {
                var booking = _bookingService.GetAllBookingByCustomer(user.Id);
                return View(booking);
            }
           
        }

        public async Task<IActionResult> CreateAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            IEnumerable<ApplicationUser> customers;

            if (await _userManager.IsInRoleAsync(user, SD.Role_Admin))
            {
                customers = await _userManager.GetUsersInRoleAsync("Customer");
            }
            else
            {
                customers = new List<ApplicationUser> { user };

            }
         
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
                
                var slot = _slotService.GetSlotById(obj.Booking.SlotId);
                if (!_bookingService.checkBookingExitByCustomer(obj.Booking))
                {
                    if (_bookingService.getBookingCountOnDate(obj.Booking) <= slot.MaxPatients)
                    {
                     
                        _bookingService.CreateBooking(obj.Booking);
                        TempData["success"] = "The Booking has been created successfully.";
                      
                        obj.Booking.Slot=slot;
                        obj.Booking.Customer = await _userManager.FindByIdAsync(obj.Booking.CustomerId);
                        BackgroundJob.Enqueue(() => _emailService.SendEmailConfirmation(obj.Booking.Customer, obj.Booking));
                       
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["error"] = "Booking limit for the selected date has been reached.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["error"] = "You have already made a booking for this slot.";
                    return RedirectToAction(nameof(Index));
                }
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
                
                BackgroundJob.Enqueue(() => _emailService.SendEmailConfirmation(objFromDb.Customer, objFromDb));
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The booking could not be cancle.";
            return View();
            
        }
  
    }
}
