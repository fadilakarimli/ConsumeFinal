using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Country
{
    public class CountryEdit
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string Name { get; set; }

    }
}
