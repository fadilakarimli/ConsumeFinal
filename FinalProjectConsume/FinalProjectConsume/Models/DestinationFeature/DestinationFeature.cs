using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.DestinationFeature
{
    public class DestinationFeature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TourCount { get; set; }
        public decimal PriceFrom { get; set; }
        public string IconImage { get; set; } // Url or path of the image

    }
}
