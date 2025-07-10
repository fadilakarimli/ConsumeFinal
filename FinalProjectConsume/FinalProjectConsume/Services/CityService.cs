using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.Country;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class CityService : ICityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/City/";
        public CityService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(CityCreate model)
        {
            return await _httpClient.PostAsJsonAsync($"{_baseUrl}Create", model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, CityEdit model)
        {
            return await _httpClient.PutAsJsonAsync($"{_baseUrl}Edit/{id}", model);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<City>>($"{_baseUrl}GetAll");
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<City>($"{_baseUrl}GetById/{id}");
        }
    }
}
