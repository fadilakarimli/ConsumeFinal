using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using static FinalProjectConsume.ViewComponents.BasketViewComponent;

[Route("payment")]
public class PaymentController : Controller
{
    [HttpPost("create-checkout-session")]
    public IActionResult CreateCheckoutSession()
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 5000, 
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Test Ödəniş"
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = "https://localhost:7014/payment/success",
            CancelUrl = "https://localhost:7014/payment/cancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Redirect(session.Url);
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        return View();
    }

    [HttpGet("cancel")]
    public IActionResult Cancel()
    {
        return View();
    }

    [HttpPost("create-checkout-session-basket")]
    public async Task<IActionResult> CreateCheckoutSessionBasket([FromBody] BasketVM basket)
    {
        if (basket == null || basket.BasketProducts == null || !basket.BasketProducts.Any())
            return BadRequest("Basket is empty.");

        var lineItems = new List<SessionLineItemOptions>();

        foreach (var item in basket.BasketProducts)
        {
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Price * 100), // Stripe üçün qiymət sentlə
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName
                    },
                },
                Quantity = item.Quantity,
            });
        }

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "https://localhost:7014/payment/success",
            CancelUrl = "https://localhost:7014/payment/cancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Json(new { url = session.Url });
    }

}
