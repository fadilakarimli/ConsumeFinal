using FinalProjectConsume.ViewModels;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRolesAsync();
        Task<bool> UpdateRoleAsync(string roleId, string newName);
        Task<bool> DeleteRoleAsync(string roleId);

        Task<IEnumerable<RoleVM>> GetAllAsync();
    }
}
