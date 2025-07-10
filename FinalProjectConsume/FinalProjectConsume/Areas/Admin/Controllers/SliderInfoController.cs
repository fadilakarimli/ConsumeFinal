using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class SliderInfoController : Controller
    {
        private readonly ISliderInfoService _sliderInfoService;

        public SliderInfoController(ISliderInfoService sliderInfoService)
        {
            _sliderInfoService = sliderInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sliderInfos = await _sliderInfoService.GetAllAsync();
            return View(sliderInfos);
        }

        [HttpPost]
        [Route("Admin/SliderInfo/Delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _sliderInfoService.DeleteAsync(id);
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
        public async Task<IActionResult> Create(SliderInfoCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _sliderInfoService.CreateAsync(model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error occurred");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sliderInfo = await _sliderInfoService.GetByIdAsync(id);
            if (sliderInfo == null)
                return NotFound();

            var model = new SliderInfoEdit
            {
                Title = sliderInfo.Title,
                Description = sliderInfo.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SliderInfoEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _sliderInfoService.EditAsync(id, model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var sliderInfo = await _sliderInfoService.GetByIdAsync(id);
            if (sliderInfo == null)
                return NotFound();

            return View(sliderInfo);
        }
    }
}
