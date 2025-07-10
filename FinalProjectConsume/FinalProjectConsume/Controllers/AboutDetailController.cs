using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class AboutDetailController : Controller
    {
        private readonly IAboutBlogService _aboutBlogService;
        private readonly ISettingService _settingService;

        public AboutDetailController(IAboutBlogService aboutBlogService, ISettingService settingService = null)
        {
            _aboutBlogService = aboutBlogService;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var aboutBlog = await _aboutBlogService.GetByIdAsync(id);
            if (aboutBlog == null) return NotFound();

            var allBlogs = await _aboutBlogService.GetAllAsync();

            var relatedBlogs = allBlogs
                .Where(b => b.Id != aboutBlog.Id)
                .OrderByDescending(b => b.CreatedDate)
                .Take(3)
                .ToList();
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();

            var vm = new AboutDetailVM
            {
                AboutBlog = aboutBlog, 
                Settings = settings

            };

            return View(vm);
        }
    }
}
