using FinalProjectConsume.Models.Paginate;
using FinalProjectConsume.Models.Plan;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IActivityService _activityService;
        private readonly IAmenityService _amenityService;
        private readonly ICountryService _countryService;
        private readonly IExperienceService _experienceService;
        private readonly ICityService _cityService;
        private readonly HttpClient _httpClient;


        public TourController(ITourService tourService , IAmenityService amenityService , IActivityService activityService, ICountryService countryService
                           , IExperienceService experienceService, ICityService cityService)
        {
            _tourService = tourService;
            _activityService = activityService;
            _amenityService = amenityService;
            _countryService = countryService;
            _experienceService = experienceService;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var paginatedTours = await _tourService.GetPaginatedAsync(page, 6);
            return View(paginatedTours);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var activities = await _activityService.GetAllAsync();
            var cities = await _cityService.GetAllAsync();
            var amenities = await _amenityService.GetAllAsync();
            var countries = await _countryService.GetAllAsync();
            //var experiences = await _experienceService.GetAllAsync(); 

            ViewBag.Cities = cities
                 .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                 .ToList();

            ViewBag.Activities = activities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            ViewBag.Amenities = amenities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            ViewBag.Countries = countries
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            //ViewBag.Experiences = experiences
            //    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            //    .ToList(); // 🔹 ƏLAVƏ

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TourCreate model)
        {
            var response = await _tourService.CreateAsync(model);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage);

                // ViewBag-ları yenidən doldur (çünki səhifə reload olmayacaq):
                var activities = await _activityService.GetAllAsync();
                var cities = await _cityService.GetAllAsync();
                var amenities = await _amenityService.GetAllAsync();
                var countries = await _countryService.GetAllAsync();

                ViewBag.Cities = cities
                     .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                     .ToList();

                ViewBag.Activities = activities
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                    .ToList();

                ViewBag.Amenities = amenities
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                    .ToList();

                ViewBag.Countries = countries
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                    .ToList();

                return View(model);
            }

            return RedirectToAction("Index"); 
        }




        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();

            var activities = await _activityService.GetAllAsync();
            var cities = await _cityService.GetAllAsync();
            var amenities = await _amenityService.GetAllAsync();
            var countries = await _countryService.GetAllAsync();

            var model = new TourEdit
            {
                Id = tour.Id,
                Name = tour.Name,
                Duration = tour.Duration,
                CountryCount = tour.CountryCount,
                Price = tour.Price,
                OldPrice = tour.OldPrice,
                Capacity = tour.Capacity,
                Desc = tour.Desc,
                CityIds = tour.CityIds ?? new List<int>(),
                CountryIds = tour.CountryIds ?? new List<int>(),
                ActivityIds = tour.ActivityIds ?? new List<int>(),
                AmenityIds = tour.AmenityIds ?? new List<int>(),
                //ExistingImageUrl = tour.ImageUrl,
                StartTime = DateTime.ParseExact(tour.StartDate, "MM.dd.yyyy", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact(tour.EndDate, "MM.dd.yyyy", CultureInfo.InvariantCulture),

                Cities = new SelectList(cities, "Id", "Name"),
                Countries = new SelectList(countries, "Id", "Name"),
                Activities = new SelectList(activities, "Id", "Name"),
                Amenities = new SelectList(amenities, "Id", "Name")
            };

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id, [FromForm] TourEdit model)
        {
            if (!ModelState.IsValid)
            {
                var activities = await _activityService.GetAllAsync();
                var cities = await _cityService.GetAllAsync();
                var amenities = await _amenityService.GetAllAsync();
                var countries = await _countryService.GetAllAsync();

                ViewBag.Cities = cities.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                ViewBag.Activities = activities.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                ViewBag.Amenities = amenities.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                ViewBag.Countries = countries.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

                return View(model);
            }

            var response = await _tourService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Yeniləmə zamanı xəta baş verdi.");

            var acts = await _activityService.GetAllAsync();
            var cts = await _cityService.GetAllAsync();
            var amns = await _amenityService.GetAllAsync();
            var cns = await _countryService.GetAllAsync();

            ViewBag.Cities = cts.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Activities = acts.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Amenities = amns.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Countries = cns.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View(model);
        }



        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _tourService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var tourDto = await _tourService.GetByIdAsync(id);

            if (tourDto == null)
                return NotFound();

            var cities = await _cityService.GetAllAsync();

            var tourCities = cities.Where(c => tourDto.CityIds.Contains(c.Id)).ToList();

            var countryNames = tourCities
                .Select(c => c.CountryName)
                .Distinct()
                .ToList();

            var tour = new Tour
            {
                Id = tourDto.Id,
                Name = tourDto.Name,
                Duration = tourDto.Duration,
                CountryCount = countryNames.Count,
                Desc = tourDto.Desc,
                Price = tourDto.Price,
                Capacity = tourDto.Capacity,
                OldPrice = tourDto.OldPrice,
                ImageUrl = tourDto.ImageUrl,
                CityNames = tourDto.CityNames,
                ActivityNames = tourDto.ActivityNames,
                Amenities = tourDto.Amenities,
                ExperienceNames = tourDto.ExperienceNames,
                Plans = tourDto.Plans?.Select(p => new Plan
                {
                    Day = p.Day,
                    Title = p.Title,
                    Description = p.Description
                }).ToList(),
                CountryNames = countryNames,
                StartDate = tourDto.StartDate,
                EndDate = tourDto.EndDate
            };

            return View(tour);
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Search()
        {
            var cities = await _cityService.GetAllAsync();
            var activities = await _activityService.GetAllAsync(); 

            var vm = new TourSearchRequest
            {
                AvailableCities = cities,  
                AvailableActivities = activities 
            };

            return View(vm);
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> SearchResults(List<int> CityIds, List<int> ActivityIds, int? Capacity, DateTime? StartDate)
        {
            var searchRequest = new TourSearchRequest
            {
                //Name = Name,
                CityIds = CityIds ?? new List<int>(),
                ActivityIds = ActivityIds ?? new List<int>(),
                Capacity = Capacity,
                StartDate = StartDate,
            };

            var results = await _tourService.SearchAsync(searchRequest);

            return View(results);
        }



    }
}
