using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var teamMembers = await _teamMemberService.GetAllAsync();
            return View(teamMembers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamMemberCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _teamMemberService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Xəta baş verdi.");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teamMember = await _teamMemberService.GetByIdAsync(id);
            if (teamMember == null) return NotFound();

            var model = new TeamMemberEdit
            {
                FullName = teamMember.FullName,
                Position = teamMember.Position,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamMemberEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _teamMemberService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _teamMemberService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var teamMember = await _teamMemberService.GetByIdAsync(id);
            if (teamMember == null) return NotFound();
            return View(teamMember);
        }

    }
}
