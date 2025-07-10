using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class SpecialOfferModelController : Controller
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOfferModelController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var offers = await _specialOfferService.GetAllAsync();
            return View(offers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SpecialOfferCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _specialOfferService.CreateAsync(model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Xəta baş verdi, təkrar cəhd edin");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _specialOfferService.GetByIdAsync(id);
            if (offer == null)
                return NotFound();

            var model = new SpecialOfferEdit
            {
                TitleSmall = offer.TitleSmall,
                TitleMain = offer.TitleMain
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SpecialOfferEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _specialOfferService.EditAsync(id, model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin");
            return View(model);
        }

        [HttpPost]
        [Route("Admin/SpecialOffer/Delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _specialOfferService.DeleteAsync(id);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var offer = await _specialOfferService.GetByIdAsync(id);
            if (offer == null)
                return NotFound();

            return View(offer);
        }


    }
}
