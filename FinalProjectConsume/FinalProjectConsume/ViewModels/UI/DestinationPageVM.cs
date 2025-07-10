using FinalProjectConsume.Models.TrandingDestination;

namespace FinalProjectConsume.ViewModels.UI
{
    public class DestinationPageVM
    {
        public IEnumerable<TrandingDestination> TrandingDestinations { get; set; }
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<SettingVM> Settings { get; set; }


    }
}
