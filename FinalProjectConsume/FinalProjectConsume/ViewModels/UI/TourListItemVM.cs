namespace FinalProjectConsume.ViewModels.UI
{
    public class TourListItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public string StartDate { get; set; } // "yyyy-MM-dd"
        public string CityName { get; set; }
    }
}
