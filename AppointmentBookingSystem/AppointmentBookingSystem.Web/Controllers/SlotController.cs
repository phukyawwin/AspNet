using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentBookingSystem.Web.Controllers
{
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
    }
}
