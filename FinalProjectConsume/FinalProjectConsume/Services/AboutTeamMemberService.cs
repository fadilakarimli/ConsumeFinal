using FinalProjectConsume.Models.AboutTeamMember;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class AboutTeamMemberService : IAboutTeamMemberService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/AboutTeamMember/";
        public AboutTeamMemberService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(AboutTeamMemberCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            content.Add(new StringContent(model.Position ?? ""), "Position");
            content.Add(new StringContent(model.FullName ?? ""), "FullName");
            content.Add(new StringContent(model.About ?? ""), "About");

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, AboutTeamMemberEdit model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            content.Add(new StringContent(model.Position ?? ""), "Position");
            content.Add(new StringContent(model.FullName ?? ""), "FullName");
            content.Add(new StringContent(model.About ?? ""), "About");

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<AboutTeamMember>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AboutTeamMember>>($"{_baseUrl}GetAll");
        }

        public async Task<AboutTeamMember> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AboutTeamMember>($"{_baseUrl}GetById/{id}");
        }
    }
}
