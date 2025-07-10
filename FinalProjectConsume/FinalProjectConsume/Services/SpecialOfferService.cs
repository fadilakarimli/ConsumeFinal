using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/SpecialOfferModel/";
        public SpecialOfferService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(SpecialOfferCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.TitleSmall), "TitleSmall");
            content.Add(new StringContent(model.TitleMain), "TitleMain");

            if (model.BackgroundImage != null)
            {
                var stream = model.BackgroundImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.BackgroundImage.ContentType);
                content.Add(fileContent, "BackgroundImageUrl", model.BackgroundImage.FileName);
            }

            if (model.DiscountImage != null)
            {
                var stream = model.DiscountImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.DiscountImage.ContentType);
                content.Add(fileContent, "DiscountImageUrl", model.DiscountImage.FileName);
            }

            if (model.BagImage != null)
            {
                var stream = model.BagImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.BagImage.ContentType);
                content.Add(fileContent, "BagImageUrl", model.BagImage.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete?id={id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, SpecialOfferEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.TitleSmall), "TitleSmall");
            content.Add(new StringContent(model.TitleMain), "TitleMain");

            if (model.BackgroundImage != null)
            {
                var stream = model.BackgroundImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.BackgroundImage.ContentType);
                content.Add(fileContent, "BackgroundImageUrl", model.BackgroundImage.FileName);
            }

            if (model.DiscountImage != null)
            {
                var stream = model.DiscountImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.DiscountImage.ContentType);
                content.Add(fileContent, "DiscountImageUrl", model.DiscountImage.FileName);
            }

            if (model.BagImage != null)
            {
                var stream = model.BagImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.BagImage.ContentType);
                content.Add(fileContent, "BagImageUrl", model.BagImage.FileName);
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<SpecialOffer>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SpecialOffer>>($"{_baseUrl}GetAll");
        }

        public async Task<SpecialOffer> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SpecialOffer>($"{_baseUrl}GetById/{id}");
        }
    }
}
