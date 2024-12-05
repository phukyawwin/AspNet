using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentBookingSystem.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _docterService;
        private readonly ISpecialtyService _specialtyService;
        public DoctorController(IDoctorService docterService, ISpecialtyService specialtyService)
        {
            _docterService = docterService;
            _specialtyService = specialtyService;
        }


        public IActionResult Index()
        {
            var docters = _docterService.GetAllDoctors();
            return View(docters);
        }

        public IActionResult Create()
        {
            DoctorVM docterVM = new()
            {
                SpecialtyList = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(docterVM);
        }

        [HttpPost]
        public IActionResult Create(DoctorVM obj)
        {


            bool roomNumberExists = _docterService.CheckDoctorExists(obj.Docter.Id);

            if (ModelState.IsValid && !roomNumberExists)
            {
                _docterService.CreateDoctor(obj.Docter);
                TempData["success"] = "The doctor has been created successfully.";
                return RedirectToAction(nameof(Index));
            }

            if (roomNumberExists)
            {
                TempData["error"] = "The doctor already exists.";
            }
            obj.SpecialtyList = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int doctorId)
        {
            DoctorVM doctorVM = new()
            {
                SpecialtyList = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Docter = _docterService.GetDoctorById(doctorId)
            };
            if (doctorVM.Docter == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(doctorVM);
        }


        [HttpPost]
        public IActionResult Update(DoctorVM doctorVM)
        {

            if (ModelState.IsValid)
            {
                _docterService.UpdateDoctor(doctorVM.Docter);
                TempData["success"] = "The docter has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            doctorVM.SpecialtyList = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(doctorVM);
        }



        public IActionResult Delete(int doctorId)
        {
            DoctorVM doctorVM = new()
            {
                SpecialtyList = _specialtyService.GetAllSpecialtys().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Docter = _docterService.GetDoctorById(doctorId)
            };
            if (doctorVM.Docter == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(doctorVM);
        }



        [HttpPost]
        public IActionResult Delete(DoctorVM doctorVM)
        {
            Doctor? objFromDb = _docterService.GetDoctorById(doctorVM.Docter.Id);
            if (objFromDb is not null)
            {
                _docterService.DeleteDoctor(objFromDb.Id);
                TempData["success"] = "The docter has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The docter could not be deleted.";
            return View();
        }
    }
}
