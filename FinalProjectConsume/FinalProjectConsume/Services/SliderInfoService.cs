using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/SliderInfo/";

        public SliderInfoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> CreateAsync(SliderInfoCreate model)
        {
            return await _httpClient.PostAsJsonAsync($"{_baseUrl}Create", model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }
        public async Task<HttpResponseMessage> EditAsync(int id, SliderInfoEdit model)
        {
            return await _httpClient.PutAsJsonAsync($"{_baseUrl}Edit/{id}", model);
        }

        public async Task<IEnumerable<SliderInfo>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SliderInfo>>($"{_baseUrl}GetAll");
        }

        public async Task<SliderInfo> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SliderInfo>($"{_baseUrl}GetById/{id}");
        }
    }
}
