using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.ViewModels.UI
{
    public class ContactVM
    {
        public string SearchTerm { get; set; }

        [Required(ErrorMessage = "Email daxil edilməlidir")]
        [EmailAddress(ErrorMessage = "Email düzgün deyil")]
        public string email { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi daxil edilməlidir")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Ad soyad daxil edilməlidir")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Mesaj daxil edilməlidir")]
        public string message { get; set; }

        public int id { get; set; }

        public List<SettingVM> Settings { get; set; }
    }
}
