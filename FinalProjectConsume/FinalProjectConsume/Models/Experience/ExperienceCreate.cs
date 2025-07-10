using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Experience
{
    public class ExperienceCreate
    {
        public string Name { get; set; }
        public int TourId { get; set; }
    }
}
