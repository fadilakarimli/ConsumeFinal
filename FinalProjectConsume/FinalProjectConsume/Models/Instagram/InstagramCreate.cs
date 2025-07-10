using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Instagram
{
    public class InstagramCreate
    {
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
