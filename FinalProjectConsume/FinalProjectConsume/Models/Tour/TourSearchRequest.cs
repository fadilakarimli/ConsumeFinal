namespace FinalProjectConsume.Models.Tour
{
    public class TourSearchRequest
    {
        //public string? Name { get; set; }
        public List<int>? CityIds { get; set; }
        public List<int>? ActivityIds { get; set; }
        public int? Capacity { get; set; }
        public DateTime? StartDate { get; set; }
        public IEnumerable<FinalProjectConsume.Models.City.City> AvailableCities { get; set; } = new List<FinalProjectConsume.Models.City.City>();
        public IEnumerable<FinalProjectConsume.Models.Activity.Activity> AvailableActivities { get; set; } = new List<FinalProjectConsume.Models.Activity.Activity>();

    }

}
