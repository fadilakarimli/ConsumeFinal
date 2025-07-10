using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectConsume.Models.Plan
{
    public class PlanEdit
    {
        public int Day { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TourId { get; set; }

        public List<SelectListItem> Tours { get; set; } = new List<SelectListItem>();
    }
}
