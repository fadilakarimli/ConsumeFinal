using FinalProjectConsume.Models.AboutApp;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AboutAppController : Controller
    {
        private readonly IAboutAppService _aboutAppService;

        public AboutAppController(IAboutAppService aboutAppService)
        {
            _aboutAppService = aboutAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var aboutApps = await _aboutAppService.GetAllAsync();
            return View(aboutApps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutAppCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _aboutAppService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutApp = await _aboutAppService.GetByIdAsync(id);
            if (aboutApp == null)
                return NotFound();

            var model = new AboutAppEdit
            {
                Title = aboutApp.Title,
                Desc = aboutApp.Desc,
                Text = aboutApp.Text
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutAppEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _aboutAppService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _aboutAppService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var aboutApp = await _aboutAppService.GetByIdAsync(id);
            if (aboutApp == null)
                return NotFound();

            return View(aboutApp);
        }
    }
}
