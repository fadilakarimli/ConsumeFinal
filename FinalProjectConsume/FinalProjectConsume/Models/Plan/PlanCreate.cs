using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Plan
{
    public class PlanCreate
    {
        [Range(1, int.MaxValue, ErrorMessage = "Day 1 or more than lower")]
        public int Day { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TourId { get; set; }
        public List<SelectListItem>? Tours { get; set; } // Tur siyahısı üçün
    }
}
