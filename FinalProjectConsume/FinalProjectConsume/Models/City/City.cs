namespace FinalProjectConsume.Models.City
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; } // buraya əlavə et
        public string CountryName { get; set; } // əgər göstərəcəksənsə
    }
}
