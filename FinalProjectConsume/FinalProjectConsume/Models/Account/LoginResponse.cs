    namespace FinalProjectConsume.Models.Account
    {
        public class LoginResponse
        {
            public bool Success { get; set; }
            public string? Error { get; set; }
            public string? Token { get; set; }
            public string? UserName { get; set; } // optional
            public List<string>? Roles { get; set; } // bunu əlavə et
            public string? UserId { get; set; }
        }
    }
