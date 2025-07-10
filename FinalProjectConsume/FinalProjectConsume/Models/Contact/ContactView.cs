using FinalProjectConsume.Models.Setting;

namespace FinalProjectConsume.Models.Contact
{
    public class ContactView
    {
        public IEnumerable<GetSetting> Settings { get; set; }
        public PostContact ContactMessage { get; set; }
    }
}
