using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Tour
{
    public class TourEdit
    {
        public int Id { get; set; }  // Vacib
        public string Name { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Capacity { get; set; }
        public string Desc { get; set; }

        public List<int> CityIds { get; set; } = new List<int>();
        public List<int> ActivityIds { get; set; } = new List<int>();
        public List<int> AmenityIds { get; set; } = new List<int>();
        public List<int> CountryIds { get; set; } = new List<int>();

        public IFormFile? ImageFile { get; set; }
        //public string? ExistingImageUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        // For checkbox dropdowns in the View
        public SelectList? Cities { get; set; }
        public SelectList? Countries { get; set; }
        public SelectList? Activities { get; set; }
        public SelectList? Amenities { get; set; }
    }

}
