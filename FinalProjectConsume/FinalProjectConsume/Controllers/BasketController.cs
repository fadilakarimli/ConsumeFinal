using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using static FinalProjectConsume.ViewComponents.BasketViewComponent;

namespace FinalProjectConsume.Controllers
{
    public class BasketController : Controller
    {
        private readonly HttpClient _httpClient;

        public BasketController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AuthToken");
            BasketVM basketVm = null;

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userid = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


                if (!string.IsNullOrEmpty(userid))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    var res = await _httpClient.GetAsync($"/api/admin/Basket/GetBasketByUserId/{userid}");
                    if (res.IsSuccessStatusCode)
                    {
                        basketVm = await res.Content.ReadFromJsonAsync<BasketVM>(options);
                    }
                }
            }

            return View(basketVm);
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity([FromBody] BasketUpVM model)
        {
            string token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return Json(new { success = false, message = "User is not logged in." });

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "User ID not found in token." });

            model.UserId = userId;

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7145/api/admin/Basket/IncreaseQuantity", model);
            return response.IsSuccessStatusCode
                ? Json(new { success = true })
                : Json(new { success = false, message = "Failed to update quantity." });
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity([FromBody] BasketUpVM model)
        {
            string token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return Json(new { success = false, message = "User is not logged in." });

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "User ID not found in token." });

            model.UserId = userId;

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7145/api/admin/Basket/DecreaseQuantity", model);
            return response.IsSuccessStatusCode
                ? Json(new { success = true })
                : Json(new { success = false, message = "Failed to update quantity." });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProductFromBasket(int productId)
        {
            string token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userid = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userid))
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _httpClient.DeleteAsync($"/api/admin/Basket/DeleteProductFromBasket?productId={productId}&userId={userid}");

            if (response.IsSuccessStatusCode)
            {
                return Ok(new { message = "Product removed successfully." });
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Error while removing the product: {errorMessage}");
            }
        }
    }
}
