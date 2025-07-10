using FinalProjectConsume.Models.Contact;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            // Əvvəlcə köhnə mesajları arxivlə
            await client.PostAsync("https://localhost:7145/api/admin/Contact/ArchiveOld/ArchiveOld", null);

            // Sonra bütün mesajları çək
            var response = await client.GetAsync("https://localhost:7145/api/admin/Contact/GetAll");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "An error occurred while loading messages.");
                return View(new List<ContactVM>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var contactList = JsonConvert.DeserializeObject<List<PostContact>>(jsonData);

            var viewModelList = contactList
                .Where(x => x.isArchived == false) // 🔸 yalnız arxivdə olmayanları göstər
                .Select(x => new ContactVM
                {
                    id = x.id,
                    email = x.email,
                    phoneNumber = x.phoneNumber,
                    fullName = x.fullName,
                    message = x.message
                }).ToList();

            return View(viewModelList);
        }

        [HttpGet]
        public async Task<IActionResult> Archive()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7145/api/admin/Contact/GetAll");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "An error occurred while loading archive.");
                return View("Archive", new List<ContactVM>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var contactList = JsonConvert.DeserializeObject<List<PostContact>>(jsonData);

            var viewModelList = contactList
                .Where(x => x.isArchived == true) // 🔸 yalnız arxivdə olanlar
                .Select(x => new ContactVM
                {
                    id = x.id,
                    email = x.email,
                    phoneNumber = x.phoneNumber,
                    fullName = x.fullName,
                    message = x.message
                }).ToList();

            return View("Archive", viewModelList);
        }




        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7145/api/admin/Contact/GetById/{id}"); 
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Mesaj tapılmadı.");
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<ContactVM>(jsonData);
            return View(contact);
        }


        [HttpPost]
        public async Task<IActionResult> Reply(int id, string replyMessage)
        {
            if (string.IsNullOrWhiteSpace(replyMessage))
            {
                ModelState.AddModelError("", "Reply message cannot be empty.");
                return RedirectToAction("Details", new { id });
            }

            var client = _httpClientFactory.CreateClient();

            var requestUrl = $"https://localhost:7145/api/admin/Contact/Reply?contactId={id}&replyMessage={Uri.EscapeDataString(replyMessage)}";

            var response = await client.PostAsync(requestUrl, null); 

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to reply to the message.");
                return RedirectToAction("Details", new { id });
            }

            TempData["Success"] = "Reply sent successfully!";
            return RedirectToAction("Details", new { id });
        }

    }
}
