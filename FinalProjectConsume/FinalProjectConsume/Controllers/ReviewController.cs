using FinalProjectConsume.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145") 
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitReview(ReviewCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View("Error");

            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7145/api/Review/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Success");
            }

            return View("Error");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
