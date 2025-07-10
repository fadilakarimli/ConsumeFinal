using FinalProjectConsume.Models.AboutBlog;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AboutBlogController : Controller
    {
        private readonly IAboutBlogService _aboutBlogService;

        public AboutBlogController(IAboutBlogService aboutBlogService)
        {
            _aboutBlogService = aboutBlogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var aboutBlogs = await _aboutBlogService.GetAllAsync();
            return View(aboutBlogs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutBlogCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _aboutBlogService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutBlog = await _aboutBlogService.GetByIdAsync(id);
            if (aboutBlog == null)
                return NotFound();

            var model = new AboutBlogEdit
            {
                Name = aboutBlog.Name,
                Desc = aboutBlog.Desc
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutBlogEdit request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var response = await _aboutBlogService.EditAsync(id, request);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _aboutBlogService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var aboutBlog = await _aboutBlogService.GetByIdAsync(id);
            if (aboutBlog == null)
                return NotFound();

            return View(aboutBlog);
        }
    }
}
