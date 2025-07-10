using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Product
{
    public class ProductCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile Image { get; set; }
    }
}
