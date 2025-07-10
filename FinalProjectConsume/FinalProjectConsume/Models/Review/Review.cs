namespace FinalProjectConsume.Models.Review
{
    public class Review
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public int? Star { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public int TourId { get; set; }  // Hansı tura aiddir
    }

}
