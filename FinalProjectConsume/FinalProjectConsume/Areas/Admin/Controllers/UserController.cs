using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class UserController : Controller
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("SuperAdmin") && !User.HasClaim("Permission", "ShowUser"))
            {
                return Forbid();
            }

            var users = await _accountService.GetAllUsersAsync();
            return View(users);
        }


        [HttpPost]

        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var result = await _accountService.AssignRoleAsync(userId, roleName);

            if (result)
                TempData["Success"] = "Rol uğurla təyin olundu!";
            else
                TempData["Error"] = "Xəta baş verdi.";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]

        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var success = await _accountService.RemoveRoleAsync(userId, roleName);

            if (success)
                TempData["Success"] = "Rol uğurla silindi!";
            else
                TempData["Error"] = "Xəta baş verdi.";

            return RedirectToAction(nameof(Index));
        }





    }
}
