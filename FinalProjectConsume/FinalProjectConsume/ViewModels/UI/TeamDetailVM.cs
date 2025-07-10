using FinalProjectConsume.Models.AboutTeamMember;
using FinalProjectConsume.Models.TeamMember;

namespace FinalProjectConsume.ViewModels.UI
{
    public class TeamDetailVM
    {
        public AboutTeamMember AboutTeamMember { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<SettingVM> Settings { get; set; } = new List<SettingVM>();


    }
}
