using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Slider
{
    public class SliderCreate
    {
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Img { get; set; }
    }
}
