namespace FinalProjectConsume.ViewModels.UI
{
    public class BookingCreateVM
    {
        public int TourId { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public string UserEmail { get; set; }
        public decimal Price { get; set; }
    }
}
