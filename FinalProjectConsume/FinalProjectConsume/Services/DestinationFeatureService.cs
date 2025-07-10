using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class DestinationFeatureService : IDestinationFeatureService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/DestinationFeature/";
        public DestinationFeatureService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> CreateAsync(DestinationFeatureCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");
            content.Add(new StringContent(model.TourCount.ToString()), "TourCount");
            content.Add(new StringContent(model.PriceFrom.ToString()), "PriceFrom");

            if (model.IconImage != null)
            {
                var stream = model.IconImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.IconImage.ContentType);
                content.Add(fileContent, "IconImage", model.IconImage.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }


        public async Task<HttpResponseMessage> EditAsync(int id, DestinationFeatureEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");
            content.Add(new StringContent(model.TourCount.ToString()), "TourCount");
            content.Add(new StringContent(model.PriceFrom.ToString()), "PriceFrom");

            if (model.IconImage != null)
            {
                var stream = model.IconImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.IconImage.ContentType);
                content.Add(fileContent, "IconImage", model.IconImage.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }


        public async Task<IEnumerable<DestinationFeature>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DestinationFeature>>($"{_baseUrl}GetAll");
        }


        public async Task<DestinationFeature> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<DestinationFeature>($"{_baseUrl}GetById/{id}");
        }

    }
}

