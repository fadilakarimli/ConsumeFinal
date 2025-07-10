using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;

namespace FinalProjectConsume.Controllers
{
    public class NewController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly INewService _newService;
        private readonly ISettingService _settingService;
        public NewController(IBlogService blogService, INewService newService, ISettingService settingService)
        {
            _blogService = blogService; 
            _newService = newService;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int take = 3;

            var blogs = await _blogService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                blogs = blogs.Where(b => b.Title != null &&
                                         b.Title.ToLower().Contains(search.Trim().ToLower()));
            }

            int totalBlogs = blogs.Count();

            var pagedBlogs = blogs
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            int totalPages = (int)Math.Ceiling(totalBlogs / (double)take);
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();


            var vm = new NewVM
            {
                Blogs = pagedBlogs,
                SearchTerm = search ?? string.Empty,
                CurrentPage = page,
                TotalPages = totalPages,
                Settings = settings

            };

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> SearchApi(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Axtarış boş ola bilməz.");

            var blogs = await _newService.SearchBlogsAsync(query);
            return Json(blogs);
        }


    }
}
