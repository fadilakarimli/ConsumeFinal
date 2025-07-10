using FinalProjectConsume.Models.AboutTravil;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class AboutTravilService : IAboutTravilService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/AboutTravil/";
        public AboutTravilService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(AboutTravilCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            if (model.SmallImage != null)
            {
                var stream = model.SmallImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.SmallImage.ContentType);
                content.Add(fileContent, "SmallImage", model.SmallImage.FileName);
            }

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.Subtitle ?? ""), "Subtitle");
            content.Add(new StringContent(model.SubDesc ?? ""), "SubDesc");

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> EditAsync(int id, AboutTravilEdit model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            if (model.SmallImage != null)
            {
                var stream = model.SmallImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.SmallImage.ContentType);
                content.Add(fileContent, "SmallImage", model.SmallImage.FileName);
            }

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.Subtitle ?? ""), "Subtitle");
            content.Add(new StringContent(model.SubDesc ?? ""), "SubDesc");

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<IEnumerable<AboutTravil>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AboutTravil>>($"{_baseUrl}GetAll");
        }

        public async Task<AboutTravil> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AboutTravil>($"{_baseUrl}GetById/{id}");
        }
    }
}
