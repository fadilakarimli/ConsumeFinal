using FinalProjectConsume.Models.ChooseUsAbout;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class ChooseUsAboutService : IChooseUsAboutService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/ChooseUsAbout/";
        public ChooseUsAboutService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(ChooseUsAboutCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Subtitle ?? ""), "Subtitle");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.SubDesc ?? ""), "SubDesc");
            content.Add(new StringContent(model.Text ?? ""), "Text");

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

        public async Task<HttpResponseMessage> EditAsync(int id, ChooseUsAboutEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title ?? ""), "Title");
            content.Add(new StringContent(model.Subtitle ?? ""), "Subtitle");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.SubDesc ?? ""), "SubDesc");
            content.Add(new StringContent(model.Text ?? ""), "Text");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<ChooseUsAbout>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ChooseUsAbout>>($"{_baseUrl}GetAll");
        }

        public async Task<ChooseUsAbout> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ChooseUsAbout>($"{_baseUrl}GetById/{id}");
        }
    }
}
