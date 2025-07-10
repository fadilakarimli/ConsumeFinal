using FinalProjectConsume.Models.AboutApp;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class AboutAppService : IAboutAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/AboutApp/";
        public AboutAppService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(AboutAppCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            if (model.AppleImage != null)
            {
                var stream = model.AppleImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.AppleImage.ContentType);
                content.Add(fileContent, "AppleImage", model.AppleImage.FileName);
            }

            if (model.PlayStoreImage != null)
            {
                var stream = model.PlayStoreImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.PlayStoreImage.ContentType);
                content.Add(fileContent, "PlayStoreImage", model.PlayStoreImage.FileName);
            }

            if (model.BackgroundImage != null)
            {
                var stream = model.BackgroundImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.BackgroundImage.ContentType);
                content.Add(fileContent, "BackgroundImage", model.BackgroundImage.FileName);
            }

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.Text ?? ""), "Text");

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, AboutAppEdit model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            if (model.AppleImage != null)
            {
                var stream = model.AppleImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.AppleImage.ContentType);
                content.Add(fileContent, "AppleImage", model.AppleImage.FileName);
            }

            if (model.PlayStoreImage != null)
            {
                var stream = model.PlayStoreImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.PlayStoreImage.ContentType);
                content.Add(fileContent, "PlayStoreImage", model.PlayStoreImage.FileName);
            }

            if (model.BackgroundImage != null)
            {
                var stream = model.BackgroundImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.BackgroundImage.ContentType);
                content.Add(fileContent, "BackgroundImage", model.BackgroundImage.FileName);
            }

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.Text ?? ""), "Text");

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<AboutApp>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AboutApp>>($"{_baseUrl}GetAll");
        }

        public async Task<AboutApp> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AboutApp>($"{_baseUrl}GetById/{id}");
        }
    }
}
