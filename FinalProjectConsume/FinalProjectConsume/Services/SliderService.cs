using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class SliderService : ISliderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Slider/";
        public SliderService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> CreateAsync(SliderCreate model)
        {
            using var content = new MultipartFormDataContent();

            if (model.Img != null)
            {
                var stream = model.Img.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Img.ContentType);
                content.Add(fileContent, "Image", model.Img.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, SliderEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(id.ToString()), "Id");

            if (model.Img != null)
            {
                var stream = model.Img.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Img.ContentType);
                content.Add(fileContent, "Img", model.Img.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Slider>>($"{_baseUrl}GetAll");
        }
        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Slider>($"{_baseUrl}GetById/{id}");
        }
    }
}
