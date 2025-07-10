using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.NewsLetter;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Diagnostics;
using System.Drawing.Printing;

namespace FinalProjectConsume.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IInstagramService _instagramService;
        private readonly IBlogService _blogService;
        private readonly IDestinationFeatureService _destinationFeatureService;
        private readonly ITeamMemberService _teamMemberService;
        private readonly ITrandingDestinationService _trandingDestinationService;
        private readonly ITourService _tourService;
        private readonly ISliderService _sliderService;
        private readonly INewsLetterService _newsLetterService;
        private readonly ISliderInfoService _sliderInfoService;
        private readonly ISpecialOfferService _specialOfferService;
        private readonly IAboutAgencyService _aboutAgencyService;
        private readonly ICityService _cityService;
        private readonly IActivityService _activityService;
        private readonly IReviewService _reviewService;


        public HomeController(IBrandService brandService,
                             IInstagramService instagramService, IBlogService blogService,
                             IDestinationFeatureService destinationFeatureService,
                             ITeamMemberService teamMemberService,
                             ITrandingDestinationService trandingDestinationService, ITourService tourService
                           , ISliderService sliderService, INewsLetterService newsLetterService, ISliderInfoService sliderInfoService, 
                            ISpecialOfferService specialOfferService, IAboutAgencyService aboutAgencyService, ICityService cityService,     
                            IActivityService activityService, IReviewService reviewService)
        {
            _brandService = brandService;
            _instagramService = instagramService;
            _blogService = blogService;
            _destinationFeatureService = destinationFeatureService;
            _teamMemberService = teamMemberService;
            _trandingDestinationService = trandingDestinationService;
            _tourService = tourService;
            _sliderService = sliderService;
            _newsLetterService = newsLetterService;
            _sliderInfoService = sliderInfoService;
            _specialOfferService = specialOfferService;
            _aboutAgencyService = aboutAgencyService;
            _cityService = cityService;
            _activityService = activityService;
            _reviewService = reviewService;
        }
        public async Task<IActionResult> Index(string search)
        {
            var brands = await _brandService.GetAllAsync();
            var instagrams = await _instagramService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();
            var destinationFeatures = await _destinationFeatureService.GetAllAsync();
            var teamMembers = await _teamMemberService.GetAllAsync();
            var trandingDestinatons = await _trandingDestinationService.GetAllAsync();
            var tours = await _tourService.GetAllAsync();
            var sliders = await _sliderService.GetAllAsync();
            var sliderInfos = await _sliderInfoService.GetAllAsync();
            var specialOffer = await _specialOfferService.GetAllAsync();
            var aboutAgency = await _aboutAgencyService.GetAllAsync();
            var city = await _cityService.GetAllAsync();
            var activity = await _activityService.GetAllAsync();
            var review = await _reviewService.GetAllAsync();


            var model = new HomePageVM
            {
                Brands = brands.ToList(),
                Instagrams = instagrams.ToList(),
                Blogs = blogs.Take(3).ToList(),
                DestinationFeatures = destinationFeatures.ToList(),
                TeamMembers = teamMembers.ToList(),
                TrandingDestinations = trandingDestinatons.Take(6).ToList(),
                Tours = tours.ToList(),
                Sliders = sliders.ToList(),
                SliderInfos = sliderInfos.ToList(),
                SpecialOffers = specialOffer.ToList(),
                AboutAgencies = aboutAgency.ToList(),
                Cities = city.ToList(),
                Activities = activity.ToList(),
                Reviews = review.ToList(),        
                SearchTerm = search ?? string.Empty
            };

            return View(model);
        }

        public async Task<IActionResult> LoadMoreTours(int skip)
        {
            var tours = await _tourService.GetAllAsync();
            var nextTours = tours.Skip(skip).Take(4).ToList();

            var result = nextTours.Select(t => new TourListItemVM
            {
                Id = t.Id,
                Name = t.Name,
                ImageUrl = t.ImageUrl,
                Price = t.Price,
                OldPrice = t.OldPrice,
                Duration = t.Duration,
                CountryCount = t.CountryCount,
                StartDate = t.StartDate.ToString(),
                CityName = t.CityNames != null && t.CityNames.Count > 0 ? t.CityNames[0] : "No City"
            });

            return Json(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe([FromBody] SubscribePostVM model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || !model.Email.Contains("@"))
                return BadRequest("Email düzgün deyil.");

            var response = await _newsLetterService.CreateAsync(new NewsLetterCreate { Email = model.Email });

            if (response.IsSuccessStatusCode)
                return Ok("Email elave olundu");

            return StatusCode(500, "Email bazaya yazýlmadý");
        }


    }



}

