using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;   
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllAsync();
            return View(settings);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var setting = await _settingService.GetByIdAsync(id);
            if (setting == null) return NotFound();

            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SettingVM setting)
        {
            if (!ModelState.IsValid) return View(setting);

            var result = await _settingService.EditAsync(setting.Id, setting);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(setting);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
