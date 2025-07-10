using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Brand
{
    public class BrandCreate
    {
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
