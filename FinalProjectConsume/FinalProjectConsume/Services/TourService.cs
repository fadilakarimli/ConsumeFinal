using FinalProjectConsume.Models.Paginate;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Services
{
    public class TourService : ITourService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Tour/";

        public TourService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7145/");
        }
        public async Task<HttpResponseMessage> CreateAsync(TourCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Name ?? ""), "Name");
            content.Add(new StringContent(model.Duration ?? ""), "Duration");
            content.Add(new StringContent((model.CountryIds?.Count ?? 0).ToString()), "CountryCount");
            content.Add(new StringContent(model.Desc ?? ""), "Desc");
            content.Add(new StringContent(model.StartDate ?? ""), "StartDate");
            content.Add(new StringContent(model.EndDate ?? ""), "EndDate");
            content.Add(new StringContent(model.Price.ToString()), "Price");

            if (model.OldPrice != null)
                content.Add(new StringContent(model.OldPrice.Value.ToString()), "OldPrice");

            content.Add(new StringContent(model.Capacity.ToString()), "Capacity");

            foreach (var cityId in model.CityIds ?? new List<int>())
                content.Add(new StringContent(cityId.ToString()), "CityIds");

            foreach (var activityId in model.ActivityIds ?? new List<int>())
                content.Add(new StringContent(activityId.ToString()), "ActivityIds");

            foreach (var amenityId in model.AmenityIds ?? new List<int>())
                content.Add(new StringContent(amenityId.ToString()), "AmenityIds");

            foreach (var countryId in model.CountryIds ?? new List<int>())
                content.Add(new StringContent(countryId.ToString()), "CountryIds");

            // Şəkil yoxlanışı
            if (model.ImageFile != null)
            {
                var stream = model.ImageFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", model.ImageFile.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }


        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete/{id}");
        }

            public async Task<HttpResponseMessage> EditAsync(int id, TourEdit model)
            {
                using var content = new MultipartFormDataContent();

                content.Add(new StringContent(model.Name ?? ""), "Name");
                content.Add(new StringContent(model.Duration ?? ""), "Duration");
                content.Add(new StringContent(model.CountryCount.ToString()), "CountryCount");
                content.Add(new StringContent(model.Desc ?? ""), "Desc");
                content.Add(new StringContent(model.StartTime.ToString("yyyy-MM-dd")), "StartDate");
                content.Add(new StringContent(model.EndTime.ToString("yyyy-MM-dd")), "EndDate");
                content.Add(new StringContent(model.Price.ToString()), "Price");
                if (model.OldPrice.HasValue)
                    content.Add(new StringContent(model.OldPrice.Value.ToString()), "OldPrice");
                content.Add(new StringContent(model.Capacity.ToString()), "Capacity");

                foreach (var cityId in model.CityIds ?? Enumerable.Empty<int>())
                    content.Add(new StringContent(cityId.ToString()), "CityIds");

                foreach (var activityId in model.ActivityIds ?? Enumerable.Empty<int>())
                    content.Add(new StringContent(activityId.ToString()), "ActivityIds");

                foreach (var amenityId in model.AmenityIds ?? Enumerable.Empty<int>())
                    content.Add(new StringContent(amenityId.ToString()), "AmenityIds");

                foreach (var countryId in model.CountryIds ?? Enumerable.Empty<int>())
                    content.Add(new StringContent(countryId.ToString()), "CountryIds");

                if (model.ImageFile != null)
                {
                    var stream = model.ImageFile.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImageFile.ContentType);
                    content.Add(fileContent, "ImageFile", model.ImageFile.FileName);
                }

                //if (!string.IsNullOrEmpty(model.ExistingImageUrl))
                //{
                //    content.Add(new StringContent(model.ExistingImageUrl), "ExistingImageUrl");
                //}

                return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
            }


        public async Task<IEnumerable<Tour>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Tour>>($"{_baseUrl}GetAll");
        }

        public async Task<Tour> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/admin/Tour/GetById/{id}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var tour = JsonSerializer.Deserialize<Tour>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tour;
        }
        public async Task<IEnumerable<Tour>> SearchAsync(TourSearchRequest searchRequest)
        {
            var apiModel = new
            {
                Cities = string.Join(",", searchRequest.CityIds ?? new List<int>()),
                Activities = string.Join(",", searchRequest.ActivityIds ?? new List<int>()),
                Date = searchRequest.StartDate ?? DateTime.Now,
                GuestCount = searchRequest.Capacity ?? 1
            };

            var json = JsonSerializer.Serialize(apiModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/admin/Tour/Search", content);
            if (!response.IsSuccessStatusCode) return Enumerable.Empty<Tour>();

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<IEnumerable<Tour>>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? Enumerable.Empty<Tour>();
        }

        public async Task<IEnumerable<Tour>> FilterAsync(TourFilter filter)
        {
            var json = JsonSerializer.Serialize(filter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}filter", content);

            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<Tour>();
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            var tours = JsonSerializer.Deserialize<IEnumerable<Tour>>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tours ?? Enumerable.Empty<Tour>();
        }


        public async Task<Paginated<Tour>> GetPaginatedAsync(int page = 1, int take = 5)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7145/api/admin/Tour/GetPaginated/paginated?page={page}&take={take}");
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Paginated<Tour>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

            


    }
}
