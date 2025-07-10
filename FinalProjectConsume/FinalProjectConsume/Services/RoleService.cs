using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels;
using Newtonsoft.Json;

namespace FinalProjectConsume.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://localhost:7145/api/Account";

        public RoleService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> CreateRolesAsync()
        {
            var response = await _client.PostAsync($"{_baseUrl}/CreateRoles", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateRoleAsync(string roleId, string newName)
        {
            var response = await _client.PutAsync($"{_baseUrl}/UpdateRole?roleId={roleId}&newName={newName}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var response = await _client.DeleteAsync($"https://localhost:7145/api/admin/Account/DeleteRole/DeleteRole?roleId{roleId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<IEnumerable<RoleVM>> GetAllAsync()
        {
            var response = await _client.GetAsync($"https://localhost:7145/api/admin/Account/GetAllRoles");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<RoleVM>>(content);
        }
    }
}
