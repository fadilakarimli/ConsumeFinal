using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sldiers = await _sliderService.GetAllAsync();
            return View(sldiers);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _sliderService.DeleteAsync(id);
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
        public async Task<IActionResult> Create(SliderCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Img == null)
            {
                ModelState.AddModelError("Img", "Image is required");
                return View(model);
            }

            var response = await _sliderService.CreateAsync(model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error occurred");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new SliderEdit { Id = id };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SliderEdit model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var response = await _sliderService.EditAsync(id, model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null)
                return NotFound();

            return View(slider);
        }


    }
}
