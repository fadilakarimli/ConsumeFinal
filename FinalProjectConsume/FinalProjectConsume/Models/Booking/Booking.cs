using FinalProjectConsume.Enums;
using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.Models.Booking
{
    public class Booking
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public string UserEmail { get; set; }
        public int Status { get; set; }
        public string ConfirmationCode { get; set; }
        public string StartDate { get; set; }


        public FinalProjectConsume.Models.Tour.Tour Tour { get; set; }

    }
}
