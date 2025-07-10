using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class InstagramService : IInstagramService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Instagram/";
        public InstagramService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> CreateAsync(InstagramCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, InstagramEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(id.ToString()), "Id");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }
            else
            {
                content.Add(new StringContent(model.ImageUrl ?? ""), "ImageUrl");
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }


        public async Task<IEnumerable<Instagram>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Instagram>>($"{_baseUrl}GetAll");
        }
        public async Task<Instagram> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Instagram>($"{_baseUrl}GetById/{id}");
        }
    }
}
