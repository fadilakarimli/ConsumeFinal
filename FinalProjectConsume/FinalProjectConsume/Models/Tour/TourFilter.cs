namespace FinalProjectConsume.Models.Tour
{
    public class TourFilter
    {
        public List<int>? CityIds { get; set; }
        public List<int>? ActivityIds { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? GuestCount { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<int>? AmenityIds { get; set; }
    }

}
