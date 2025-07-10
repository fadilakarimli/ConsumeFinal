using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProjectConsume.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly HttpClient _httpClient;

       
        public ShopController(IProductService productService)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145")
            };
            _productService = productService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var products = await _productService.GetAllAsync();

            var vm = new ShopVM
            {

                Products = products,
                SearchTerm = search ?? string.Empty,

            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId)
        {
            string token = HttpContext.Session.GetString("AuthToken");
            if (token == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userid = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var req = new
            {
                UserId = userid,
                ProductId = productId
            };

            var jsonData = JsonConvert.SerializeObject(req);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PostAsync("/api/admin/Basket/AddBasket", stringContent);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }


    }
}
