namespace FinalProjectConsume.Models.Contact
{
    public class GetContact
    {
        public int id { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string fullName { get; set; }
        public string message { get; set; }
        public DateTime createdDate { get; set; }   
    }
}
