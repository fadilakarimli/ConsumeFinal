using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IRolePermissionService _rolePermissionService;

        public RoleController(IRoleService roleService, IRolePermissionService rolePermissionService)
        {
            _roleService = roleService;
            _rolePermissionService = rolePermissionService;
        }

        public async Task<IActionResult> CreateRoles()
        {
            var success = await _roleService.CreateRolesAsync();
            if (success)
                TempData["Message"] = "Rollar uğurla yaradıldı.";
            else
                TempData["Error"] = "Rol yaratmaq alınmadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(string roleId, string newName)
        {
            var success = await _roleService.UpdateRoleAsync(roleId, newName);
            if (success)
                TempData["Message"] = "Rol yeniləndi.";
            else
                TempData["Error"] = "Yenilənmə uğursuz oldu.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var success = await _roleService.DeleteRoleAsync(roleId);
            if (success)
                TempData["Message"] = "Rol silindi.";
            else
                TempData["Error"] = "Silinmə zamanı xəta baş verdi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }


        public async Task<IActionResult> Permissions(string roleName)
        {
            var permissions = await _rolePermissionService.GetPermissionsByRoleName(roleName);
            if (permissions == null)
            {
                // Əgər role permission yoxdursa, yeni boş VM göndər
                permissions = new RolePermissionVM();
            }

            ViewBag.RoleName = roleName;
            return View(permissions);
        }

        // RolePermission-ları update etmək üçün POST action
        [HttpPost]
        public async Task<IActionResult> Permissions(string roleName, RolePermissionVM model)
        {
            var success = await _rolePermissionService.UpdatePermissionsByRoleName(roleName, model);

            if (success)
                TempData["Message"] = $"{roleName} rolunun icazələri yeniləndi.";
            else
                TempData["Error"] = "İcazələrin yenilənməsində xəta baş verdi.";

            return RedirectToAction("Index");
        }
    }
}
