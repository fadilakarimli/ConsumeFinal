using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.ViewModels
{
    public class UserPasswordVM
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string confirmPassword { get; set; }
    }
}
