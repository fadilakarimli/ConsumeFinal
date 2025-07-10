using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class DestinationDetailController : Controller
    {
        private readonly ITrandingDestinationService _trandingDestinationService;
        private readonly ISettingService _settingService;

        public DestinationDetailController(ITrandingDestinationService trandingDestinationService, ISettingService settingService)
        {
            _trandingDestinationService = trandingDestinationService;   
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(int id, string search)
        {
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();

            var destination = await _trandingDestinationService.GetByIdAsync(id);
            if (destination == null) return NotFound();

            var vm = new DestinationDetailVM
            {
                TrandingDestination = destination,
                SearchTerm = search ?? string.Empty,
                Settings = settings

            };

            return View(vm);
        }
    }
}
