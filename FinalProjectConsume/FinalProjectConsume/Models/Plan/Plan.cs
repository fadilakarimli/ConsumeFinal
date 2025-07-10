namespace FinalProjectConsume.Models.Plan
{
    public class Plan
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TourId { get; set; }

        public string? TourName { get; set; }
    }
}
