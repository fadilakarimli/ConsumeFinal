using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class AboutController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IChooseUsAboutService _chooseUsAboutService;
        private readonly IAboutTeamMemberService _aboutTeamMemberService;  
        private readonly IAboutAppService _aboutAppService;
        private readonly IAboutDestinationService _aboutDestinationService;
        private readonly IAboutBlogService _aboutBlogService;
        private readonly IAboutTravilService _aboutTravilService;
        private readonly ISettingService _settingService;


        public AboutController(IBrandService brandService, IChooseUsAboutService chooseUsAboutService
                             , IAboutTeamMemberService aboutTeamMemberService, IAboutAppService aboutAppService,
                              IAboutDestinationService aboutDestinationService, IAboutBlogService aboutBlogService,
                             IAboutTravilService aboutTravilService, ISettingService settingService)
        {
            _brandService = brandService;
            _chooseUsAboutService = chooseUsAboutService;
            _aboutTeamMemberService = aboutTeamMemberService;
            _aboutAppService = aboutAppService;
            _aboutDestinationService = aboutDestinationService;
            _aboutBlogService = aboutBlogService;
            _aboutTravilService = aboutTravilService;
            _settingService = settingService;
        }
        public async Task <IActionResult> Index(string search)
        {
            var brands = await _brandService.GetAllAsync();
            var chooseUsAbout = await _chooseUsAboutService.GetAllAsync();
            var aboutTeamMember = await _aboutTeamMemberService.GetAllAsync();
            var aboutApp = await _aboutAppService.GetAllAsync();
            var aboutDest = await _aboutDestinationService.GetAllAsync();
            var aboutBlogs = await _aboutBlogService.GetAllAsync();
            var aboutTravil = await _aboutTravilService.GetAllAsync();
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();

            var model = new AboutPageVM
            {
                Brands = brands.ToList(),
                ChooseUsAbouts = chooseUsAbout.ToList(),
                AboutTeamMembers = aboutTeamMember.Take(4).ToList(),    
                AboutApps = aboutApp.ToList(),
                AboutDestinations = aboutDest.ToList(),
                AboutBlogs = aboutBlogs.OrderByDescending(b => b.CreatedDate) // ← tarixə görə tərsinə sırala
                    .Take(3)
                    .ToList(),
                AboutTravils = aboutTravil.ToList(),
                SearchTerm = search ?? string.Empty,
                Settings = settings

            };


            return View(model);
        }
    }
}
