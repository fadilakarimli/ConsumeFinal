namespace FinalProjectConsume.ViewModels
{
    public class ReviewListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int Star { get; set; }
        public int TourId { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? ReplyMessage { get; set; }
    }
}
