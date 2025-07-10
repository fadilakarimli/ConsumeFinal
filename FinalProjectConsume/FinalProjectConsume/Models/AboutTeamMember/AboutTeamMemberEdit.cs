using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.AboutTeamMember
{
    public class AboutTeamMemberEdit
    {
        public IFormFile? Image { get; set; }
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string Position { get; set; }
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string FullName { get; set; }
        public string About { get; set; }
    }
}   
