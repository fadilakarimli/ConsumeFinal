using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IAmenityService _amenityService;
        private readonly IActivityService _activityService;
        private readonly ICityService _cityService;
        private readonly ISettingService _settingService;
        private readonly HttpClient _httpClient;

        public TourController(ITourService tourService
                           , IAmenityService amenityService,
                             IActivityService activityService,
                             ICityService cityService,
                             ISettingService settingService)
        {
            _tourService = tourService;
            _amenityService = amenityService;
            _activityService = activityService;
            _cityService = cityService;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145")
            };
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(int page = 1,  string? search = null,int? cityId = null,int? activityId = null,string? departureDate = null,   int? guestCount = null)
        {
            int take = 6;

            var amenities = await _amenityService.GetAllAsync();
            var activities = await _activityService.GetAllAsync();
            var cities = await _cityService.GetAllAsync();

            var tours = await _tourService.GetAllAsync();

            // Axtarış
            if (!string.IsNullOrWhiteSpace(search))
            {
                tours = tours.Where(t => t.Name != null && t.Name.ToLower().Contains(search.Trim().ToLower()));
            }

            // City adı ilə filter (CityNames listində varsa)
            if (cityId.HasValue)
            {
                var selectedCity = cities.FirstOrDefault(c => c.Id == cityId.Value);
                if (selectedCity != null)
                {
                    tours = tours.Where(t => t.CityNames != null && t.CityNames.Contains(selectedCity.Name));
                }
            }

            // Activity adı ilə filter (ActivityNames listində varsa)
            if (activityId.HasValue)
            {
                var selectedActivity = activities.FirstOrDefault(a => a.Id == activityId.Value);
                if (selectedActivity != null)
                {
                    tours = tours.Where(t => t.ActivityNames != null && t.ActivityNames.Contains(selectedActivity.Name));
                }
            }

            // StartDate (Departure) ilə filter
            if (!string.IsNullOrWhiteSpace(departureDate))
            {
                if (DateTime.TryParseExact(departureDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    tours = tours.Where(t =>
                        DateTime.TryParse(t.StartDate, out DateTime startDate)
                        && startDate.Date == parsedDate.Date);
                }
            }

            // Guest count (Capacity) ilə filter
            if (guestCount.HasValue)
            {
                tours = tours.Where(t => t.Capacity >= guestCount.Value);
            }

            // Pagination
            int totalTours = tours.Count();
            var pagedTours = tours.Skip((page - 1) * take).Take(take).ToList();
            int totalPages = (int)Math.Ceiling(totalTours / (double)take);
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();


            var model = new TourPageVM
            {
                Tours = pagedTours,
                Amenities = amenities.ToList(),
                Activities = activities.ToList(),
                Cities = cities.ToList(),
                SearchTerm = search ?? string.Empty,
                CurrentPage = page,
                TotalPages = totalPages,

                // UI-də inputları doldurmaq üçün
                SelectedCityId = cityId,
                SelectedActivityId = activityId,
                SelectedDepartureDate = departureDate,
                SelectedGuestCount = guestCount,
                Settings = settings
            };

            return View(model);
        }





        [HttpGet]
        public async Task<IActionResult> SearchByName(string? search)
        {
            var searchQuery = search ?? string.Empty;
            var searchResponse = await _httpClient.GetAsync($"/api/client/Tour/SearchByName?name={searchQuery}");
            var searchJson = await searchResponse.Content.ReadAsStringAsync();
            var searchObject = JsonSerializer.Deserialize<List<TourPageVM>>(searchJson);

            return Json(searchObject);
        }

        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] TourFilter filter)
        {
            var json = JsonSerializer.Serialize(filter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7145/api/Tour/Filter/filter", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Filter failed");
            }
            Console.WriteLine($"Filter status: {response.StatusCode}");
            var resultJson = await response.Content.ReadAsStringAsync();
            return Content(resultJson, "application/json");
        }

        //[HttpGet]
        //public async Task<IActionResult> Paginated(int page = 1)
        //{
        //    int take = 6;
        //    var pagedResult = await _tourService.GetPaginatedAsync(page, take);

        //    var model = new TourPageVM
        //    {
        //        Tours = pagedResult.Items.ToList(),
        //        CurrentPage = page,
        //        TotalPages = pagedResult.TotalPages
        //    };

        //    return View(model);
        //}




    }
}
