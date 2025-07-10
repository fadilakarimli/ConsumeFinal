using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FinalProjectConsume.Helpers
{
    public static class ClaimsHelper
    {
        public static async Task RefreshClaimsAsync(HttpContext context, List<Claim> claims)
        {
            await context.SignOutAsync("CookieAuth");

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await context.SignInAsync("CookieAuth", principal);
        }
    }

}
