using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.TeamMember
{
    public class TeamMemberEdit
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string FullName { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string Position { get; set; }

        public IFormFile? Image { get; set; }
    }
}
