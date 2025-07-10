using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Amenity;
using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.ViewModels.UI
{
    public class TourPageVM
    {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<Amenity> Amenities { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public string? SearchTerm { get; set; }


        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public int? SelectedCityId { get; set; }
        public int? SelectedActivityId { get; set; }
        public string? SelectedDepartureDate { get; set; }
        public int? SelectedGuestCount { get; set; }
        public IEnumerable<SettingVM> Settings { get; set; } = new List<SettingVM>();







    }
}
