using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class TeamController : Controller
    {
        //private readonly ITeamMemberService _teamMemberService;
        private readonly IAboutTeamMemberService _aboutTeamMemberService;
        private readonly ISettingService _settingService;

        public TeamController(ITeamMemberService teamMemberService, IAboutTeamMemberService aboutTeamMemberService, ISettingService settingService)
        {
            //_teamMemberService = teamMemberService;
            _aboutTeamMemberService = aboutTeamMemberService;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(string search )
        {
            //var teamMembers = await _teamMemberService.GetAllAsync();
            var aboutTeamMember = await _aboutTeamMemberService.GetAllAsync();
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();

            var model = new TeamPageVM
            {
                AboutTeamMembers = aboutTeamMember.ToList(),
                SearchTerm = search ?? string.Empty,
                Settings = settings


                //TeamMembers = teamMembers.ToList(),
            };

            return View(model);
        }
    }
}
