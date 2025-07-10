using FinalProjectConsume.Models.AboutDestination;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AboutDestinationController : Controller
    {
        private readonly IAboutDestinationService _aboutDestinationService;

        public AboutDestinationController(IAboutDestinationService aboutDestinationService)
        {
            _aboutDestinationService = aboutDestinationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var aboutDestinations = await _aboutDestinationService.GetAllAsync();
            return View(aboutDestinations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutDestinationCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _aboutDestinationService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yaradılma zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutDestination = await _aboutDestinationService.GetByIdAsync(id);
            if (aboutDestination == null)
                return NotFound();

            var model = new AboutDestinationEdit
            {
                Name = aboutDestination.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutDestinationEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _aboutDestinationService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _aboutDestinationService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var aboutDestination = await _aboutDestinationService.GetByIdAsync(id);
            if (aboutDestination == null)
                return NotFound();

            return View(aboutDestination);
        }
    }
}
