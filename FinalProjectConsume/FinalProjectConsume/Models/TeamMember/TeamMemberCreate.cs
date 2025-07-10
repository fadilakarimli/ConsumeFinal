using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.TeamMember
{
    public class TeamMemberCreate
    {
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
