using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.Contact;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace FinalProjectConsume.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient = new();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISettingService _settingService;
        public ContactController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ISettingService settingService)
        {
            _httpClientFactory = httpClientFactory;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(string search)
        {
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();
            var model = new ContactVM
            {
                SearchTerm = search ?? string.Empty,
                Settings = settings
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMessage(ContactVM postContactVM)
        {
            using (var multipartContent = new MultipartFormDataContent())
            {
                if (!string.IsNullOrWhiteSpace(postContactVM.email))
                    multipartContent.Add(new StringContent(postContactVM.email), "Email");

                if (!string.IsNullOrWhiteSpace(postContactVM.phoneNumber))
                    multipartContent.Add(new StringContent(postContactVM.phoneNumber), "PhoneNumber");

                if (!string.IsNullOrWhiteSpace(postContactVM.fullName))
                    multipartContent.Add(new StringContent(postContactVM.fullName), "FullName");

                if (!string.IsNullOrWhiteSpace(postContactVM.message))
                    multipartContent.Add(new StringContent(postContactVM.message), "Message");

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(postContactVM);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await _httpClient.PostAsync("https://localhost:7145/api/Contact/CreateMessage", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorContent = await responseMessage.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the message.");
                    return RedirectToAction("Index");
                }
                    
            }

        }
    }
}
