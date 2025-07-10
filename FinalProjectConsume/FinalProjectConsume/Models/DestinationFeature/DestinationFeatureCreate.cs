using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.DestinationFeature
{

    public class DestinationFeatureCreate
    {
        [Required]
        public string Title { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tour count cannot be negative.")]
        public int TourCount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
        public decimal PriceFrom { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile IconImage { get; set; }
    }

}
