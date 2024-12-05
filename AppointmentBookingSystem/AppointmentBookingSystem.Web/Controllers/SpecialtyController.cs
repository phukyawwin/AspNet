using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingSystem.Web.Controllers
{
    [Authorize]
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }


        public IActionResult Index()
        {
            var specialty = _specialtyService.GetAllSpecialtys();
            return View(specialty);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Specialty obj)
        {

            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {

                _specialtyService.CreateSpecialty(obj);
                TempData["success"] = "The specialty has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int specialtyId)
        {
            Specialty? obj = _specialtyService.GetSpecialtyById(specialtyId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Specialty obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {

                _specialtyService.UpdateSpecialty(obj);
                TempData["success"] = "The specialty has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int specialtyId)
        {
            Specialty? obj = _specialtyService.GetSpecialtyById(specialtyId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }


        [HttpPost]
        public IActionResult Delete(Specialty obj)
        {
            bool deleted = _specialtyService.DeleteSpecialty(obj.Id);
            if (deleted)
            {
                TempData["success"] = "The specialty has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed to delete the specialty.";
            }
            return View();
        }
    }
}
