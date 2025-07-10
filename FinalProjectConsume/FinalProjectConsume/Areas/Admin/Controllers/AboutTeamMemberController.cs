using FinalProjectConsume.Models.AboutTeamMember;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AboutTeamMemberController : Controller
    {
        private readonly IAboutTeamMemberService _aboutTeamMemberService;

        public AboutTeamMemberController(IAboutTeamMemberService aboutTeamMemberService)
        {
            _aboutTeamMemberService = aboutTeamMemberService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teamMembers = await _aboutTeamMemberService.GetAllAsync();
            return View(teamMembers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutTeamMemberCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _aboutTeamMemberService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teamMember = await _aboutTeamMemberService.GetByIdAsync(id);
            if (teamMember == null) return NotFound();

            var model = new AboutTeamMemberEdit
            {
                Position = teamMember.Position,
                FullName = teamMember.FullName,
                About = teamMember.About
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutTeamMemberEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _aboutTeamMemberService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _aboutTeamMemberService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var teamMember = await _aboutTeamMemberService.GetByIdAsync(id);
            if (teamMember == null) return NotFound();

            return View(teamMember);
        }
    }
}
