using FinalProjectConsume.Models.Paginate;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FinalProjectConsume.Services
{
    public class TrandingDestinationService : ITrandingDestinationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/TrandingDestination/";
        public TrandingDestinationService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(TrandingDestinationCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }


        public async Task<HttpResponseMessage> EditAsync(int id, TrandingDestinationEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<TrandingDestination>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TrandingDestination>>($"{_baseUrl}GetAll");
        }

        public async Task<TrandingDestination> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TrandingDestination>($"{_baseUrl}GetById/{id}");
        }



        public async Task<Paginated<Tour>> GetPaginatedAsync(int page = 1, int take = 5)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}paginated?page={page}&take={take}");
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Paginated<Tour>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
