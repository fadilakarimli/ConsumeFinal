using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/TeamMember/";

        public TeamMemberService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> CreateAsync(TeamMemberCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.FullName), "FullName");
            content.Add(new StringContent(model.Position), "Position");

            if (model.Image != null)
            {
                var allowedTypes = new[]
                {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/bmp",
            "image/webp"
        };

                if (!allowedTypes.Contains(model.Image.ContentType.ToLower()))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.UnsupportedMediaType)
                    {
                        ReasonPhrase = "Only image format"
                    };
                }

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


        public async Task<HttpResponseMessage> EditAsync(int id, TeamMemberEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.FullName), "FullName");
            content.Add(new StringContent(model.Position), "Position");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<TeamMember>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TeamMember>>($"{_baseUrl}GetAll");
        }

        public async Task<TeamMember> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TeamMember>($"{_baseUrl}GetById/{id}");
        }
    }
}
