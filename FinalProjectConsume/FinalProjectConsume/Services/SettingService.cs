using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Services
{
    public class SettingService : ISettingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7145/api";

        public SettingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SettingVM>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/admin/Setting/GetAll");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<SettingVM>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<SettingVM> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/admin/Setting/GetById/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SettingVM>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<ResponseVM> EditAsync(int id, SettingVM setting)
        {
            var jsonData = JsonSerializer.Serialize(setting);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/admin/Setting/Update/{id}", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResponseVM>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
