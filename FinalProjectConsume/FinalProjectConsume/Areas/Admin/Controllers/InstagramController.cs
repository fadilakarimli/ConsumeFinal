using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class InstagramController : Controller
    {
        private readonly IInstagramService _instagramService;
        public InstagramController(IInstagramService instagramService)
        {
            _instagramService = instagramService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var instagrams = await _instagramService.GetAllAsync();
            return View(instagrams);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _instagramService.DeleteAsync(id);

            if (response.IsSuccessStatusCode)
            {
                return Ok(); 
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstagramCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _instagramService.CreateAsync(model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error occurred");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var insta = await _instagramService.GetByIdAsync(id);
            if (insta == null) return NotFound();

            var model = new InstagramEdit
            {
                Id = insta.Id,
                ImageUrl = insta.Image
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, InstagramEdit model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var response = await _instagramService.EditAsync(id, model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var insta = await _instagramService.GetByIdAsync(id);
            if (insta == null)
                return NotFound();

            return View(insta);
        }
    }
}
