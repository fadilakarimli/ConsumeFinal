using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Country;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetAllAsync();
            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _countryService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }


        [HttpPost]
        [Route("Admin/Country/Delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _countryService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _countryService.GetByIdAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _countryService.GetByIdAsync(id);
            if (data == null) return NotFound();

            var model = new CountryEdit
            {
                Name = data.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CountryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _countryService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }


    }
}
