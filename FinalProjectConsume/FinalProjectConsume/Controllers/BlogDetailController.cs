using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ISettingService _settingService;

        public BlogDetailController(IBlogService blogService, ISettingService settingService)
        {
            _blogService = blogService;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null) return NotFound();

            var allBlogs = await _blogService.GetAllAsync();

            var relatedBlogs = allBlogs
                .Where(b => b.Id != blog.Id)
                .OrderByDescending(b => b.CreatedDate)
                .Take(3)
                .ToList();
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();


            var vm = new BlogDetailVM
            {
                Blog = blog,
                RelatedBlogs = relatedBlogs, 
                Settings = settings

            };

            return View(vm);
        }
    }
}
