using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.AboutDestination
{
    public class AboutDestinationEdit
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]

        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
