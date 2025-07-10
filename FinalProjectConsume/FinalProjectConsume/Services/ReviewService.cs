using FinalProjectConsume.Models.Review;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using System.Text.Json;

namespace FinalProjectConsume.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145")
            };
        }

        public async Task<List<ReviewVM>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7145/api/Review/GetAll");
            if (!response.IsSuccessStatusCode) return new List<ReviewVM>();

            var json = await response.Content.ReadAsStringAsync();
            var reviews = JsonSerializer.Deserialize<List<Review>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (reviews == null || !reviews.Any())
                return new List<ReviewVM>();

            return reviews.Select(r => new ReviewVM
            {
                Name = r.Name,
                Message = r.Message,
                Star = r.Star ?? 0, 
                Country = string.IsNullOrEmpty(r.Country) ? "Unknown" : r.Country,
                ImageUrl = string.IsNullOrEmpty(r.ImageUrl) ? "/assets/img/testimonial/default.png" : r.ImageUrl
            }).ToList();
        }

    }

}
