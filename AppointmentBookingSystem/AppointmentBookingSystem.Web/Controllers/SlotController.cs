using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentBookingSystem.Web.Controllers
{
    [Authorize]
    public class SlotController : Controller
    {
        private readonly ISlotService _slotService;
        private readonly IDoctorService _docterService;
        
        public SlotController(ISlotService slotService, IDoctorService docterService)
        {

            _slotService = slotService;
            _docterService = docterService;
        }


        public IActionResult Index()
        {
            var slot = _slotService.GetAllSlot();
            return View(slot);
        }

        public IActionResult Create()
        {
            SlotVM slotVM = new()
            {
                DoctorList = _docterService.GetAllDoctors().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(slotVM);
        }

        [HttpPost]
        public IActionResult Create(SlotVM obj)
        {


            bool slotExists = _slotService.CheckSlotExists(obj.Slot.Id);

            if (ModelState.IsValid && !slotExists)
            {
                _slotService.CreateSlot(obj.Slot);
                TempData["success"] = "The Slot has been created successfully.";
                return RedirectToAction(nameof(Index));
            }

            if (slotExists)
            {
                TempData["error"] = "The Slot already exists.";
            }
            obj.DoctorList = _docterService.GetAllDoctors().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int slotId)
        {
            SlotVM slotVM = new()
            {
                DoctorList = _docterService.GetAllDoctors().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Slot = _slotService.GetSlotById(slotId)
            };
            if (slotVM.Slot == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(slotVM);
        }


        [HttpPost]
        public IActionResult Update(SlotVM slotVM)
        {

            if (ModelState.IsValid)
            {
                _slotService.UpdateSlot(slotVM.Slot);
                TempData["success"] = "The Slot has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            slotVM.DoctorList = _docterService.GetAllDoctors().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(slotVM);
        }
        public IActionResult Delete(int slotId)
        {
            SlotVM slotVM = new()
            {
                DoctorList = _docterService.GetAllDoctors().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Slot = _slotService.GetSlotById(slotId)
            };
            if (slotVM.Slot == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(slotVM);
        }



        [HttpPost]
        public IActionResult Delete(SlotVM slotVM)
        {
            Slot? objFromDb = _slotService.GetSlotById(slotVM.Slot.Id);
            if (objFromDb is not null)
            {
                _slotService.DeleteSlot(objFromDb.Id);
                TempData["success"] = "The slot has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The slot could not be deleted.";
            return View();
        }
       
        public IActionResult GetTimeSlots(int doctorId)
        {
            if (doctorId <= 0)
            {
                return BadRequest(new { message = "Invalid doctorId." });
            }
            var slot= _slotService.GetSlotByDoctorId(doctorId).Select(u => new SelectListItem
            {
                Text = $"{(new DateTime(2000, 1, 1).Add(u.StartTime)):hh:mm tt} - {(new DateTime(2000, 1, 1).Add(u.EndTime)):hh:mm tt}",
                Value = u.Id.ToString()
            });
            return Ok(slot);
            //return Ok(Json(_slotService.GetSlotByDoctorId(doctorId)));

        }
 
        public IActionResult GetAvailableDays(int doctorId)
        {
            var availableDays = _slotService.GetSlotByDoctorId(doctorId)
                                    .Select(s => s.DayOfWeek)
                                    .Distinct()
                                    .ToList();

            // Convert the integer values to day names
            var dayNames = availableDays.Select(day => Enum.GetName(typeof(DayOfWeek), day))
                                        .ToList();

            return Ok(dayNames);

        }
    }
}
