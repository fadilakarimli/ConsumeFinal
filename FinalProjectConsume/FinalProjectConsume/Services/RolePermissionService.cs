using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Newtonsoft.Json;
using System.Text;

namespace FinalProjectConsume.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly HttpClient _httpClient;

        public RolePermissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 🔹 1. İstifadəçiyə görə permission-ları gətir
        public async Task<RolePermissionVM> GetPermissionsByUserId(string userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7145/api/admin/RolePermission/CollectPermissions/by-user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RolePermissionVM>(json);
            }

            return null;
        }

        // 🔹 2. Role adına görə permission-ları gətir
        public async Task<RolePermissionVM> GetPermissionsByRoleName(string roleName)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7145/api/admin/RolePermission/GetPermissions/{roleName}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RolePermissionVM>(json);
            }

            return null;
        }

        // 🔹 3. Role adına görə permission-ları update et
        public async Task<bool> UpdatePermissionsByRoleName(string roleName, RolePermissionVM model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://localhost:7145/api/admin/RolePermission/UpdatePermissions/{roleName}", content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Error: " + error);
                return false;
            }
            return true;

        }
    }
}
