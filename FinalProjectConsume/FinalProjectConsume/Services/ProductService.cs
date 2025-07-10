using FinalProjectConsume.Models.Product;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/product/";

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:7145/api/admin/Product/GetAll");
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"{_baseUrl}GetById/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(ProductCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Name), "Name");
            content.Add(new StringContent(model.Price.ToString()), "Price");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PostAsync($"https://localhost:7145/api/admin/Product/Create", content);
        }

        public async Task<HttpResponseMessage> EditAsync(int id, ProductEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Name), "Name");
            content.Add(new StringContent(model.Price.ToString()), "Price");

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }
    }
}
