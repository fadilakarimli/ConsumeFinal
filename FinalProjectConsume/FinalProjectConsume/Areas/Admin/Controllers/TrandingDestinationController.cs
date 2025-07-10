using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class TrandingDestinationController : Controller
    {
        private readonly ITrandingDestinationService _trandingDestinationService;

        public TrandingDestinationController(ITrandingDestinationService trandingDestinationService)
        {
            _trandingDestinationService = trandingDestinationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var destinations = await _trandingDestinationService.GetAllAsync();
            return View(destinations);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TrandingDestinationCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _trandingDestinationService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var destination = await _trandingDestinationService.GetByIdAsync(id);
            if (destination == null) return NotFound();

            var model = new TrandingDestinationEdit
            {
                Title = destination.Title,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TrandingDestinationEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _trandingDestinationService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _trandingDestinationService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var destination = await _trandingDestinationService.GetByIdAsync(id);
            if (destination == null) return NotFound();

            return View(destination);
        }


    }
}
