namespace FinalProjectConsume.ViewModels.UI
{
    public class BookingSuccessVM
    {
        public int BookingId { get; set; }
        public string TourName { get; set; }
        public string BookingDate { get; set; }  // string kimi saxla, formatlama üçün
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string ConfirmationCode { get; set; }
        public string StartDate { get; set; } 

    }

}
