using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class DestinationFeatureController : Controller
    {
        private readonly IDestinationFeatureService _destinationFeatureService;

        public DestinationFeatureController(IDestinationFeatureService destinationFeatureService)
        {
            _destinationFeatureService = destinationFeatureService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var features = await _destinationFeatureService.GetAllAsync();
            return View(features);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(DestinationFeatureCreate model)
        {
            if (model.IconImage != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".svg" };
                var extension = Path.GetExtension(model.IconImage.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("IconImage", "Only JPG, PNG, or SVG images are allowed.");
                }
            }

            if (!ModelState.IsValid)
                return View(model);

            var response = await _destinationFeatureService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "An error occurred while creating.");
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Edit(int id)
        {
            var feature = await _destinationFeatureService.GetByIdAsync(id);
            if (feature == null) return NotFound();

            var editModel = new DestinationFeatureEdit
            {
                Id = feature.Id,
                Title = feature.Title,
                TourCount = feature.TourCount,
                PriceFrom = feature.PriceFrom
                // IconImage əlavə edilmir (çünki clientdə fayl preview edə bilmərik)
            };

            return View(editModel);
        }

        
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Edit(int id, DestinationFeatureEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _destinationFeatureService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Xəta baş verdi.");
            return View(model);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _destinationFeatureService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var feature = await _destinationFeatureService.GetByIdAsync(id);
            if (feature == null) return NotFound();

            return View(feature);
        }
    }
}

