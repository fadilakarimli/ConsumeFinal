using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.NewsLetter
{
    public class NewsLetterCreate
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
