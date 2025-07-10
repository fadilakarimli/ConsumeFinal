namespace FinalProjectConsume.Models.Contact
{
    public class PostContact
    {
        public int id { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string fullName { get; set; }
        public string message { get; set; }


        public bool isArchived { get; set; }
        public DateTime CreatedAt { get; set; } // Yeni əlavə (API-dən gəlirsə)
    }
}
