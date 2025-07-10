using FinalProjectConsume.Models.Account;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> LoginAsync(Login model);
        Task<bool> RegisterAsync(Register model);
        Task<string> VerifyEmailAsync(string email, string token);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> AssignRoleAsync(string userId, string roleName);
        Task<bool> RemoveRoleAsync(string userId, string roleName);

    }
}
