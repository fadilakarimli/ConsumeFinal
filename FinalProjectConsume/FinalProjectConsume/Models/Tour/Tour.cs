using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;
using PlanModel = FinalProjectConsume.Models.Plan;

 namespace FinalProjectConsume.Models.Tour
 {
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public decimal? OldPrice { get; set; }
        public string ImageUrl { get; set; }
        public List<string> CityNames { get; set; }
        public List<string> ActivityNames { get; set; }
        public List<string> Amenities { get; set; }
        public List<string> ExperienceNames { get; set; }
        public List<string> CountryNames { get; set; } = new();
        public List<PlanModel.Plan> Plans { get; set; }

        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public string EndDate { get; set; }
        public List<int> CityIds { get; set; }
        public List<int> ActivityIds { get; set; }
        public List<int> AmenityIds { get; set; }
        public List<int> CountryIds { get; set; }




    }
 }

