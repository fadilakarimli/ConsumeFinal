using FinalProjectConsume.Models.AboutTeamMember;
using FinalProjectConsume.Models.TeamMember;

namespace FinalProjectConsume.ViewModels.UI
{
    public class TeamPageVM
    {
        //public IEnumerable<TeamMember> TeamMembers { get; set; }
        public IEnumerable<AboutTeamMember> AboutTeamMembers { get; set; }
        public string SearchTerm { get; set; }
        public List<SettingVM> Settings { get; set; }




    }
}
    