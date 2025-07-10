using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.TrandingDestination;

namespace FinalProjectConsume.ViewModels.UI
{
    public class DestinationDetailVM
    {
        public TrandingDestination TrandingDestination { get; set; }
        public string SearchTerm { get; set; }
        public List<SettingVM> Settings { get; set; }


    }
}
