using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Country;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Country/";
        public CountryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(CountryCreate model)
        {
            return await _httpClient.PostAsJsonAsync($"{_baseUrl}Create", model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, CountryEdit model)
        {
            return await _httpClient.PutAsJsonAsync($"{_baseUrl}Edit/{id}", model);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Country>>($"{_baseUrl}GetAll");
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Country>($"{_baseUrl}GetById/{id}");
        }
    }
}
