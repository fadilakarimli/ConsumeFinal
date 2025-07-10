using FinalProjectConsume.Models.Country;
using FinalProjectConsume.Models.Experience;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Experience/";
        public ExperienceService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(ExperienceCreate model)
        {
            return await _httpClient.PostAsJsonAsync($"{_baseUrl}Create", model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, ExperienceEdit model)
        {
            return await _httpClient.PutAsJsonAsync($"{_baseUrl}Edit/{id}", model);
        }

        public async Task<IEnumerable<Experience>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Experience>>($"{_baseUrl}GetAll");
        }

        public async Task<Experience> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Experience>($"{_baseUrl}GetById/{id}");
        }

        public async Task<IEnumerable<Experience>> GetByTourIdAsync(int tourId)
        {
            return await _httpClient.GetFromJsonAsync<List<Experience>>($"https://localhost:7145/api/admin/Experience/GetByTourId/GetByTourId/{tourId}");
        }


    }
}
