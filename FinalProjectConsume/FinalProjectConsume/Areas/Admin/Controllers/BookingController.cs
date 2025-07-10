using FinalProjectConsume.Enums;
using FinalProjectConsume.Models.Booking;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookingController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145")
            };
        }
        public async Task<IActionResult> Index()
        {
            // Əvvəlcə 1 dəqiqədən köhnə cancelled booking-ləri sil
            var deleteResponse = await _httpClient.DeleteAsync("https://localhost:7145/api/Booking/DeleteOldCancelled/DeleteOldCancelled");
            // Burada istəsən yoxlama edə bilərsən ama məcburi deyil

            // Sonra bookingləri gətir
            var response = await _httpClient.GetAsync("/api/Booking/GetAll");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Error");

            var json = await response.Content.ReadAsStringAsync();

            var bookings = JsonSerializer.Deserialize<List<Booking>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int bookingId, string status)
        {
            if (!int.TryParse(status, out int statusInt) || !Enum.IsDefined(typeof(BookingStatus), statusInt))
            {
                TempData["Error"] = "Düzgün status seçilməyib";
                return RedirectToAction("Index");
            }

            var dto = new BookingStatusUpdate { Status = (BookingStatus)statusInt };

            var response = await _httpClient.PostAsJsonAsync($"/api/Booking/UpdateStatus?id={bookingId}", dto);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Status yenilənmədi";
            }
            else
            {
                TempData["Success"] = "Status uğurla yeniləndi";
            }

            return RedirectToAction("Index");
        }





    }
}
