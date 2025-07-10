using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class AboutAgencyService : IAboutAgencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/AboutAgency/";
        public AboutAgencyService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(AboutAgencyCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.Subtitle ?? ""), "Subtitle");

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }


        public async Task<HttpResponseMessage> EditAsync(int id, AboutAgencyEdit model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.Subtitle ?? ""), "Subtitle");

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<AboutAgency>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AboutAgency>>($"{_baseUrl}GetAll");
        }
        public async Task<AboutAgency> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AboutAgency>($"{_baseUrl}GetById/{id}");
        }
    }
}
