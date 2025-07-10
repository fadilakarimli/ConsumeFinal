using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using static FinalProjectConsume.ViewComponents.HeaderViewComponent;

namespace FinalProjectConsume.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string search)
        {
            var settings = await _settingService.GetAllAsync();
            return View(settings);
        }

    }
}
