using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Brand
{
    public class BrandEdit
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
