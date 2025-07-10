using FinalProjectConsume.Models.Plan;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class PlanService : IPlanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Plan/";
        public PlanService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> CreateAsync(PlanCreate model)
        {
            return await _httpClient.PostAsJsonAsync($"{_baseUrl}Create", model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, PlanEdit model)
        {
            return await _httpClient.PutAsJsonAsync($"{_baseUrl}Edit/{id}", model);
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Plan>>($"{_baseUrl}GetAll");
        }

        public async Task<Plan> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Plan>($"{_baseUrl}GetById/{id}");
        }

    }
}
