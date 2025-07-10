using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Amenity;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Amenity/";
        public AmenityService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(AmenityCreate model)
        {
            return await _httpClient.PostAsJsonAsync($"{_baseUrl}Create", model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, AmenityEdit model)
        {
            return await _httpClient.PutAsJsonAsync($"{_baseUrl}Edit/{id}", model);
        }

        public async Task<IEnumerable<Amenity>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Amenity>>($"{_baseUrl}GetAll");
        }

        public async Task<Amenity> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Amenity>($"{_baseUrl}GetById/{id}");
        }
    }
}
