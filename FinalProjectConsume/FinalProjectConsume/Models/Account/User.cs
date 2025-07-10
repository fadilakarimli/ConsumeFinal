namespace FinalProjectConsume.Models.Account
{
    public class User
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> Roles { get; set; } 
    }
}
