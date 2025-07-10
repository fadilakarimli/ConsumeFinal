using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AboutAgencyController : Controller
    {
        private readonly IAboutAgencyService _aboutAgencyService;

        public AboutAgencyController(IAboutAgencyService aboutAgencyService)
        {
            _aboutAgencyService = aboutAgencyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var aboutAgencies = await _aboutAgencyService.GetAllAsync();
            return View(aboutAgencies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutAgencyCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _aboutAgencyService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutAgency = await _aboutAgencyService.GetByIdAsync(id);
            if (aboutAgency == null) return NotFound();

            var model = new AboutAgencyEdit
            {
                Title = aboutAgency.Title,
                Desc = aboutAgency.Desc,
                Subtitle = aboutAgency.Subtitle
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutAgencyEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _aboutAgencyService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _aboutAgencyService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var aboutAgency = await _aboutAgencyService.GetByIdAsync(id);
            if (aboutAgency == null) return NotFound();

            return View(aboutAgency);
        }
    }
}
