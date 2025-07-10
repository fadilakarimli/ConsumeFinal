using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        public HeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;   
        }

        public async Task<IViewComponentResult> InvokeAsync(string search)
        {
            var settings = await _settingService.GetAllAsync();
            HeaderVMVC model = new()
            {
                Settings = settings,
                SearchTerm = search ?? string.Empty
            };

            return View(model);
        }


        public class HeaderVMVC
        {
            public IEnumerable<SettingVM> Settings { get; set; }
            public string SearchTerm { get; set; }  
        }
    }


}
