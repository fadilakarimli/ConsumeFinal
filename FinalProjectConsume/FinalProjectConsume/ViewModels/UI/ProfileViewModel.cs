using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.ViewModels.UI
{
    public class ProfileViewModel :UpdateProfileVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
