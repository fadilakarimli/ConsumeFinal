using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Account
{
    public class Login
    {
        [Required(ErrorMessage = "UserName or Email Important!!")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password Important!!")]
        public string Password { get; set; }
    }
}
