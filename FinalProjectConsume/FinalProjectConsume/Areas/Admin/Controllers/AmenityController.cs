using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Amenity;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AmenityController : Controller
    {
        private readonly IAmenityService _amenityService;
        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _amenityService.GetAllAsync();
            return View(datas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AmenityCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _amenityService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var datas = await _amenityService.GetByIdAsync(id);
            if (datas == null) return NotFound();

            var model = new AmenityEdit
            {
                Name = datas.Name
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, AmenityEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _amenityService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        [Route("Admin/Amenity/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _amenityService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _amenityService.GetByIdAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }

    }
}
