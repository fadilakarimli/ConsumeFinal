using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class TeamDetailController : Controller
    {
        private readonly IAboutTeamMemberService _aboutTeamMemberService;
        private readonly ISettingService _settingService;



        public TeamDetailController(IAboutTeamMemberService aboutTeamMemberService, ISettingService settingService)
        {
            _aboutTeamMemberService = aboutTeamMemberService;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index(int id , string search)
        {
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();

            var aboutTeam = await _aboutTeamMemberService.GetByIdAsync(id);
            if (aboutTeam == null) return NotFound();

            var vm = new TeamDetailVM
            {
                AboutTeamMember = aboutTeam,
                SearchTerm = search ?? string.Empty,
                Settings = settings

            };

            return View(vm);
        }
    }
}
