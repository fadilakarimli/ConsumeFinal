using FinalProjectConsume.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145") // Sənin API URL
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7145/api/Review/GetAll");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var json = await response.Content.ReadAsStringAsync();
            var reviews = JsonSerializer.Deserialize<List<ReviewListVM>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(reviews);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/review/delete/{id}");
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7145/api/Review/GetById/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var json = await response.Content.ReadAsStringAsync();
            var review = JsonSerializer.Deserialize<ReviewListVM>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(review);
        }


        [HttpPost]
        public async Task<IActionResult> Reply(string Email, string Name, int Id, string ReplyMessage)
        {
            var subject = $"Cavabınız üçün təşəkkürlər, {Name}!";
            var body = $"Salam {Name},\n\nSizin rəyinizə cavab olaraq yazırıq:\n\n\"{ReplyMessage}\"\n\nTəşəkkür edirik.";

            var smtpClient = new SmtpClient("smtp.gmail.com") 
            {
                Port = 587,
                Credentials = new NetworkCredential("fadilafk@code.edu.az", "mleq kwrg luex jute"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("fadilafk@code.edu.az", "Travil Website"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(Email);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                TempData["Success"] = "Mesaj göndərildi!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mesaj göndərilə bilmədi: " + ex.Message;
            }

            return RedirectToAction("Detail", new { id = Id });
        }






    }
}
