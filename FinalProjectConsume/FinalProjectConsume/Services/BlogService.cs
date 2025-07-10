using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Blog/";
        public BlogService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> CreateAsync(BlogCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");
            content.Add(new StringContent(model.ShortDescription), "ShortDescription");
            if (model.Content != null)
                content.Add(new StringContent(model.Content), "Content");
            //content.Add(new StringContent(model.Author), "Author");
            //content.Add(new StringContent(model.CommentCount.ToString()), "CommentCount");

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
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete/{id}");
        }
        public async Task<HttpResponseMessage> EditAsync(int id, BlogEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");
            content.Add(new StringContent(model.ShortDescription), "ShortDescription");
            if (model.Content != null)
                content.Add(new StringContent(model.Content), "Content");
            //content.Add(new StringContent(model.Author), "Author");
            //content.Add(new StringContent(model.CommentCount.ToString()), "CommentCount");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Blog>>($"{_baseUrl}GetAll");
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Blog>($"{_baseUrl}GetById/{id}");

        }





    }
}
