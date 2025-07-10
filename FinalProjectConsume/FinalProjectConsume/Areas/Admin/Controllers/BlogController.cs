using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

    namespace FinalProjectConsume.Areas.Admin.Controllers
    {
        [Area("Admin")]
        [Authorize(Policy = "AdminOnly")]
        public class BlogController : Controller
        {
            private readonly IBlogService _blogService;

            public BlogController(IBlogService blogService)
            {
                _blogService = blogService;
            }

            public async Task<IActionResult> Index()
            {
                var blogs = await _blogService.GetAllAsync();
                return View(blogs);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(BlogCreate model)
            {
                if (!ModelState.IsValid) return View(model);

                var response = await _blogService.CreateAsync(model);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
                return View(model);
            }
            [HttpGet]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Edit(int id)
            {
                var blog = await _blogService.GetByIdAsync(id);
                if (blog == null) return NotFound();

                var model = new BlogEdit
                {
                    Title = blog.Title,
                    ShortDescription = blog.ShortDescription,
                    Content = blog.Content,
                    //Author = blog.Author,
                    //CommentCount = blog.CommentCount
                };

                return View(model);
            }

            [HttpPost]
            [Authorize(Roles = "SuperAdmin")]

            public async Task<IActionResult> Edit(int id, BlogEdit model)
            {
                if (!ModelState.IsValid) return View(model);

                var response = await _blogService.EditAsync(id, model);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
                return View(model);
            }

            [HttpPost]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await _blogService.DeleteAsync(id);
                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Silinmə zamanı xəta baş verdi.";
                }
                return RedirectToAction(nameof(Index));
            }
            [HttpGet]
            public async Task<IActionResult> Detail(int id)
            {
                var blog = await _blogService.GetByIdAsync(id);
                if (blog == null) return NotFound();
                return View(blog);
            }
        }
    }
