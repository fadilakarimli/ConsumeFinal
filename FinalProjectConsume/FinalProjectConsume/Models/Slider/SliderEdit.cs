using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Slider
{
    public class SliderEdit
    {
        public int Id { get; set; }
        public IFormFile? Img { get; set; }
    }
}
