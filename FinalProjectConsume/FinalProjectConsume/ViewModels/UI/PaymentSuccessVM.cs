namespace FinalProjectConsume.ViewModels.UI
{
    public class PaymentSuccessVM
    {
        public string TourName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
    }
}
