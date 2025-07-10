using FinalProjectConsume.Models.AboutApp;
using FinalProjectConsume.Models.AboutBlog;
using FinalProjectConsume.Models.AboutDestination;
using FinalProjectConsume.Models.AboutTeamMember;
using FinalProjectConsume.Models.AboutTravil;
using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.ChooseUsAbout;
using FinalProjectConsume.Models.TeamMember;

namespace FinalProjectConsume.ViewModels.UI
{
    public class AboutPageVM
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<ChooseUsAbout> ChooseUsAbouts { get; set; }
        public IEnumerable<AboutTeamMember> AboutTeamMembers { get; set; }
        public IEnumerable<AboutApp> AboutApps { get; set; }
        public IEnumerable<AboutDestination> AboutDestinations { get; set; }
        public IEnumerable<AboutBlog> AboutBlogs { get; set; }
        public IEnumerable<AboutTravil> AboutTravils { get; set; }
        public string SearchTerm { get; set; }
        public List<SettingVM> Settings { get; set; }



    }
}
