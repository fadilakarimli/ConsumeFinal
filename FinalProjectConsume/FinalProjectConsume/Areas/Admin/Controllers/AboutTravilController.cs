using FinalProjectConsume.Models.AboutTravil;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AboutTravilController : Controller
    {
        private readonly IAboutTravilService _aboutTravilService;

        public AboutTravilController(IAboutTravilService aboutTravilService)
        {
            _aboutTravilService = aboutTravilService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var aboutTravils = await _aboutTravilService.GetAllAsync();
            return View(aboutTravils);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutTravilCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _aboutTravilService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutTravil = await _aboutTravilService.GetByIdAsync(id);
            if (aboutTravil == null) return NotFound();

            var model = new AboutTravilEdit
            {
                Title = aboutTravil.Title,
                Desc = aboutTravil.Desc,
                Subtitle = aboutTravil.Subtitle,
                SubDesc = aboutTravil.SubDesc
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutTravilEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _aboutTravilService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _aboutTravilService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var aboutTravil = await _aboutTravilService.GetByIdAsync(id);
            if (aboutTravil == null) return NotFound();

            return View(aboutTravil);
        }
    }
}
