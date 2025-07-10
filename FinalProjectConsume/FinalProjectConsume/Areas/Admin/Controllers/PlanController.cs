using FinalProjectConsume.Models.Plan;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        private readonly ITourService _tourService;

        public PlanController(IPlanService planService, ITourService tourService)
        {
            _planService = planService;
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var plans = (await _planService.GetAllAsync()).ToList();
            var tours = await _tourService.GetAllAsync();

            foreach (var plan in plans)
            {
                var tour = tours.FirstOrDefault(t => t.Id == plan.TourId);
                if (tour != null)
                    plan.TourName = tour.Name;
            }

            return View(plans);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Create()
        {
            // Burada turları al
            var tours = await _tourService.GetAllAsync();

            var model = new PlanCreate
            {
                Tours = tours.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(PlanCreate model)
        {
            if (!ModelState.IsValid)
            {
                // Tour siyahısını doldur
                var tours = await _tourService.GetAllAsync();
                model.Tours = tours.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

                return View(model);
            }

            var response = await _planService.CreateAsync(model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            // 🔽 Əgər duplicate oldu (409 Conflict qaytarıbsa)
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("Day", errorMessage); // Day sahəsinin altına error yazırıq
            }
            else
            {
                ModelState.AddModelError("", "Plan yaratmaq zamanı xəta baş verdi.");
            }

            // Tour siyahısını yenidən doldur (çünki səhifəyə qayıdırıq)
            var allTours = await _tourService.GetAllAsync();
            model.Tours = allTours.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null) return NotFound();

            var tours = await _tourService.GetAllAsync();

            var model = new PlanEdit
            {
                Day = plan.Day,
                Title = plan.Title,
                Description = plan.Description,
                TourId = plan.TourId,
                Tours = tours.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlanEdit model)
        {
            if (!ModelState.IsValid)
            {
                var tours = await _tourService.GetAllAsync();
                model.Tours = tours.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

                return View(model);
            }

            // Manual convert PlanEdit to PlanEditDto
            var dto = new PlanEdit
            {
                Day = model.Day,
                Title = model.Title,
                Description = model.Description,
                TourId = model.TourId
            };

            var response = await _planService.EditAsync(id, dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("Day", errorMessage);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Plan yenilənmə zamanı xəta baş verdi.");
            }

            var allTours = await _tourService.GetAllAsync();
            model.Tours = allTours.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _planService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null) return NotFound();

            return View(plan);
        }
    }
}