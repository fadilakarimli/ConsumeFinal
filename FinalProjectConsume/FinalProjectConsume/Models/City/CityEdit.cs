using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.City
{
    public class CityEdit
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string Name { get; set; }
        public int CountryId { get; set; }

    }
}
