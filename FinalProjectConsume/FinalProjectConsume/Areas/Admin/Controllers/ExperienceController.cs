using FinalProjectConsume.Models.Experience;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ExperienceController : Controller
    {
        private readonly IExperienceService _experienceService;
        private readonly ITourService _tourService;

        public ExperienceController(IExperienceService experienceService, ITourService tourService)
        {
            _experienceService = experienceService;
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var experiences = await _experienceService.GetAllAsync();   
            return View(experiences);
        }

        [HttpGet]
        public async Task <IActionResult> Create()
        {

            var tours = await _tourService.GetAllAsync(); // Bütün turlar
            ViewBag.TourList = new SelectList(tours, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExperienceCreate model)
        {
            if (!ModelState.IsValid)
            {
                var tours = await _tourService.GetAllAsync();
                ViewBag.TourList = new SelectList(tours, "Id", "Name");
                return View(model);
            }

            var response = await _experienceService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "An error occurred while creating.");

            var toursAgain = await _tourService.GetAllAsync();
            ViewBag.TourList = new SelectList(toursAgain, "Id", "Name");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var experience = await _experienceService.GetByIdAsync(id);
            if (experience == null) return NotFound();

            // Model mapping (Experience -> ExperienceEdit) əgər lazımdırsa
            var model = new ExperienceEdit
            {
                Name = experience.Name,
                TourId = experience.TourId
            };

            var tours = await _tourService.GetAllAsync();
            ViewBag.TourList = new SelectList(tours, "Id", "Name", model.TourId); // seçilmiş tour

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ExperienceEdit model)
        {
            if (!ModelState.IsValid)
            {
                var tours = await _tourService.GetAllAsync();
                ViewBag.TourList = new SelectList(tours, "Id", "Name", model.TourId);
                return View(model);
            }

            var response = await _experienceService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "An error occurred while saving.");

            var toursAgain = await _tourService.GetAllAsync();
            ViewBag.TourList = new SelectList(toursAgain, "Id", "Name", model.TourId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _experienceService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var experience = await _experienceService.GetByIdAsync(id);
            if (experience == null)
                return NotFound();

            var tour = await _tourService.GetByIdAsync(experience.TourId);
            experience.TourName = tour?.Name ?? "Unknown";

            return View(experience);
        }
    }
}
