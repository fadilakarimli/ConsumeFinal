using FinalProjectConsume.Models.NewsLetter;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class NewLetterController : Controller
    {
        private readonly INewsLetterService _newsletterService;

        public NewLetterController(INewsLetterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var model = new NewsLetterCreate { Email = email };
                var response = await _newsletterService.CreateAsync(model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "You have successfully subscribed!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong while subscribing.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please enter a valid email address.";
            }

            return RedirectToAction("Index", "Home");  
        }

    }
}
