using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class ActivityService : IActivityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Activity/";
        public ActivityService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(ActivityCreate model)
        {
            return await _httpClient.PostAsJsonAsync($"{_baseUrl}Create", model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, ActivityEdit model)
        {
            return await _httpClient.PutAsJsonAsync($"{_baseUrl}Edit/{id}", model);
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Activity>>($"{_baseUrl}GetAll");
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Activity>($"{_baseUrl}GetById/{id}");
        }
    }
}
