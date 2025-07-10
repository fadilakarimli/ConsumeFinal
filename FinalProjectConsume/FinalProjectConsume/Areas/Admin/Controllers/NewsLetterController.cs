using FinalProjectConsume.Models.NewsLetter;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class NewsLetterController : Controller
    {
        private readonly INewsLetterService _newsletterService;
        public NewsLetterController(INewsLetterService newsLetterService)
        {
            _newsletterService = newsLetterService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var emails = await _newsletterService.GetAllAsync();
            return View(emails);
        }

        [HttpPost]
        [Route("Admin/Newsletter/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
            {
            var response = await _newsletterService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return Ok();

            return BadRequest();
        }

    }
}
