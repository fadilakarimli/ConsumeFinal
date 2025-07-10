using FinalProjectConsume.Models.AboutBlog;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class AboutBlogService : IAboutBlogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/AboutBlog/";
        public AboutBlogService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(AboutBlogCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            content.Add(new StringContent(model.Name ?? ""), "Name");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> EditAsync(int id, AboutBlogEdit model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            content.Add(new StringContent(model.Name ?? ""), "Name");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<IEnumerable<AboutBlog>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AboutBlog>>($"{_baseUrl}GetAll");
        }

        public async Task<AboutBlog> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AboutBlog>($"{_baseUrl}GetById/{id}");
        }
    }
}
