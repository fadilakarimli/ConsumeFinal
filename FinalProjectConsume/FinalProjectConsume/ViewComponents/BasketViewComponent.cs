using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace FinalProjectConsume.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient = new();
        public BasketViewComponent(IConfiguration configuration)
        {
            _httpClient.BaseAddress = new Uri(configuration["BaseUrl"]);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string token = HttpContext.Session.GetString("AuthToken");
            string userid = string.Empty;
            BasketVM basketVm = null;
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                userid = jwtToken.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var res = await _httpClient.GetAsync($"https://localhost:7145/api/admin/Basket/GetBasketByUserId/{userid}");
                if (res.IsSuccessStatusCode)
                {
                    basketVm = await res.Content.ReadFromJsonAsync<BasketVM>(options);
                }
            }
            return View(basketVm);
        }


        public class BasketVM
        {
            public string AppUserId { get; set; }
            public int TotalProductCount { get; set; }
            public decimal TotalPrice { get; set; }
            public List<BasketProduct> BasketProducts { get; set; }
        }

        public class BasketProduct
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductImage { get; set; }
            public decimal Price { get; set; }
          
            public int Quantity { get; set; }
        }
    }
}