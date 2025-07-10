using FinalProjectConsume.Models.ChooseUsAbout;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class ChooseUsAboutController : Controller
    {
        private readonly IChooseUsAboutService _chooseUsAboutService;

        public ChooseUsAboutController(IChooseUsAboutService chooseUsAboutService)
        {
            _chooseUsAboutService = chooseUsAboutService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _chooseUsAboutService.GetAllAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChooseUsAboutCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _chooseUsAboutService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _chooseUsAboutService.GetByIdAsync(id);
            if (item == null) return NotFound();

            var model = new ChooseUsAboutEdit
            {
                Title = item.Title,
                Subtitle = item.Subtitle,
                Desc = item.Desc,
                SubDesc = item.SubDesc,
                Text = item.Text
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ChooseUsAboutEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _chooseUsAboutService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _chooseUsAboutService.DeleteAsync(id);
            if (!response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var item = await _chooseUsAboutService.GetByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }
    }
}
