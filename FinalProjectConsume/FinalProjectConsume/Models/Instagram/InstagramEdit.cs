using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Instagram
{
    public class InstagramEdit
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; } // Əvvəlki şəkil yolu
    }
}
