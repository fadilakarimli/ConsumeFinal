using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.Country;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        public CityController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cities = await _cityService.GetAllAsync();
            return View(cities);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var countries = await _countryService.GetAllAsync();

            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityCreate model)
        {
            if (!ModelState.IsValid)
            {
                var countries = await _countryService.GetAllAsync();
                ViewBag.Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }

            var response = await _cityService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");

            var countriesReload = await _countryService.GetAllAsync();
            ViewBag.Countries = countriesReload.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [Route("Admin/City/Delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _cityService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _cityService.GetByIdAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null) return NotFound();

            var model = new CityEdit
            {
                Name = city.Name,
                CountryId = city.CountryId  
            };

            ViewBag.Countries = new SelectList(await _countryService.GetAllAsync(), "Id", "Name", model.CountryId);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, CityEdit model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Countries = new SelectList(await _countryService.GetAllAsync(), "Id", "Name", model.CountryId);
                return View(model);
            }

            var response = await _cityService.EditAsync(id, model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error occured while updating city.");
            ViewBag.Countries = new SelectList(await _countryService.GetAllAsync(), "Id", "Name", model.CountryId);
            return View(model);
        }


    }
}
