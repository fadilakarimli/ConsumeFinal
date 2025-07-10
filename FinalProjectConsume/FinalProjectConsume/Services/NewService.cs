using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http;

namespace FinalProjectConsume.Services
{
    public class NewService : INewService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/Blog/";
        public NewService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<IEnumerable<Blog>> SearchBlogsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Blog>();

            var url = $"{_baseUrl}Search?query={Uri.EscapeDataString(query)}";
            return await _httpClient.GetFromJsonAsync<List<Blog>>(url);
        }

    }
}
