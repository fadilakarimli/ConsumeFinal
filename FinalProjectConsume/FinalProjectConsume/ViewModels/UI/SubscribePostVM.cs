using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.ViewModels.UI
{
    public class SubscribePostVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
