using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.SpecialOffer
{
    public class SpecialOfferEdit
    {

        [Required(ErrorMessage = "Small Title is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]

        public string TitleSmall { get; set; }

        [Required(ErrorMessage = "Main Title is required")]
        public string TitleMain { get; set; }

        public IFormFile? BackgroundImage { get; set; }
        public IFormFile? DiscountImage { get; set; }
        public IFormFile? BagImage { get; set; }

        public string? CurrentBackgroundImageUrl { get; set; }
        public string? CurrentDiscountImageUrl { get; set; }
        public string? CurrentBagImageUrl { get; set; }
    }
}

