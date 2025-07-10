using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.SpecialOffer
{
    public class SpecialOfferCreate
    {
        [Required(ErrorMessage = "Small Title is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]

        public string TitleSmall { get; set; }

        [Required(ErrorMessage = "Main Title is required")]
        public string TitleMain { get; set; }

        [Required(ErrorMessage = "Please upload background image")]
        public IFormFile BackgroundImage { get; set; }

        [Required(ErrorMessage = "Please upload discount image")]
        public IFormFile DiscountImage { get; set; }

        [Required(ErrorMessage = "Please upload bag image")]
        public IFormFile BagImage { get; set; }
    }
}
