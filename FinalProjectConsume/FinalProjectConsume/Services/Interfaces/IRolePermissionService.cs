using FinalProjectConsume.ViewModels.UI;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IRolePermissionService
    {
        Task<RolePermissionVM> GetPermissionsByUserId(string userId);
        Task<RolePermissionVM> GetPermissionsByRoleName(string roleName);
        Task<bool> UpdatePermissionsByRoleName(string roleName, RolePermissionVM model);
    }
}
