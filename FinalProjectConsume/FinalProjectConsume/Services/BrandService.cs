using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Services
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Brand/";
        public BrandService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Brand>>($"{_baseUrl}GetAll");
        }
        public async Task<HttpResponseMessage> CreateAsync(BrandCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> EditAsync(int id, BrandEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(id.ToString()), "Id");
            content.Add(new StringContent(model.ImageUrl ?? ""), "ImageUrl"); 

            if (model.Image != null)
            {
                var stream = model.Image.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }



        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Brand>($"{_baseUrl}GetById/{id}");
        }


    }
}
