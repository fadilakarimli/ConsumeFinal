    using FinalProjectConsume.ViewModels.UI;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json;
    using System.Text;
    using FinalProjectConsume.Models.Booking;
    using FinalProjectConsume.Models.Tour;
    using FinalProjectConsume.Enums;

    namespace FinalProjectConsume.Controllers
    {
        public class BookingController : Controller
        {
            private readonly HttpClient _httpClient;

            public BookingController()
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7145")  
                };
            }

            [HttpPost]
            public IActionResult Create(BookingCreateVM vm)
            {
                if (string.IsNullOrWhiteSpace(vm.UserEmail) && User.Identity.IsAuthenticated)
                {
                    vm.UserEmail = User.Identity.Name;
                }

                TempData["BookingCreateVM"] = JsonSerializer.Serialize(vm);
                TempData.Keep("BookingCreateVM");
            
                decimal totalPrice = vm.Price*vm.AdultsCount + (vm.Price - 50)*vm.ChildrenCount;

                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
            {
                new Stripe.Checkout.SessionLineItemOptions
                {
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = totalPrice * 100,
                        Currency = "usd",
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Tour Booking"
                        },
                    },
                    Quantity = 1
                }
            },
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/booking/success",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/booking/error"
                };

                var service = new Stripe.Checkout.SessionService();
                var session = service.Create(options);

                return Redirect(session.Url);
            }

            public async Task<IActionResult> Success()
            {
                if (!TempData.ContainsKey("BookingCreateVM"))
                    return RedirectToAction("Error");

                TempData.Keep("BookingCreateVM");

                var json = TempData["BookingCreateVM"] as string;
                var vm = JsonSerializer.Deserialize<BookingCreateVM>(json);

                var jsonContent = JsonSerializer.Serialize(vm);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7145/api/Booking/CreateBooking", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("❌ Booking failed.");
                    return RedirectToAction("Error");
                }

                var resultJson = await response.Content.ReadAsStringAsync();
                var booking = JsonSerializer.Deserialize<Booking>(resultJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Console.WriteLine($"✅ Booking create: ID = {booking.Id}");

                return View(new BookingSuccessVM
                {
                    TourName = booking.Tour?.Name ?? booking.TourName,
                    BookingDate = !string.IsNullOrEmpty(booking.Tour?.StartDate)
               ? booking.Tour.StartDate
               : booking.BookingDate.ToString("dd.MM.yyyy"),
                    AdultsCount = booking.AdultsCount,
                    ChildrenCount = booking.ChildrenCount,
                    TotalPrice = booking.TotalPrice,
                    ConfirmationCode = booking.ConfirmationCode
                });

            }


            public IActionResult Error()
            {
                return View();
            }
        }

    }
