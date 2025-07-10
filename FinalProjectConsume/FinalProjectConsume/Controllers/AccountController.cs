using FinalProjectConsume.Helpers;
using FinalProjectConsume.Models.Account;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IAccountService _accountService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly string _apiBaseUrl = "/api";
         private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {

            PropertyNameCaseInsensitive = true
        };
        public AccountController(IAccountService accountService, IHttpClientFactory httpClientFactory, HttpClient httpClient, IConfiguration configuration, IRolePermissionService rolePermissionService)
        {
            _accountService = accountService;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
            _rolePermissionService = rolePermissionService;
  
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new Login());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginSuccess = await _accountService.LoginAsync(model);
            var content = await loginSuccess.Content.ReadAsStringAsync();

            if (!loginSuccess.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                return View(model);
            }

            var loginResponse = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (loginResponse != null && loginResponse.Success)
            {
                //Tokeni sessiyada saxla
                //Dəyişdir
                //Response.Cookies.Append("AuthToken", loginResponse.Token, new CookieOptions
                //{
                //    HttpOnly = true,
                //    Secure = true,
                //    SameSite = SameSiteMode.Strict,
                //    Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                //});

                HttpContext.Session.SetString("AuthToken", loginResponse.Token);
                var roles = loginResponse.Roles;

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginResponse.UserName ?? model.UserNameOrEmail)
        };

                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                // Burada permission-ləri servis vasitəsilə götürürük
                var permission = await _rolePermissionService.GetPermissionsByUserId(loginResponse.UserId);

                if (permission != null)
                {
                    // Sessiyada saxla
                    HttpContext.Session.SetString("Permissions", JsonConvert.SerializeObject(permission));

                    // Hər permission üçün claim əlavə et
                    void AddPermissionClaim(bool hasPermission, string permissionName)
                    {
                        if (hasPermission)
                            claims.Add(new Claim("Permission", permissionName));
                    }

                    AddPermissionClaim(permission.ShowAdminPanel, nameof(permission.ShowAdminPanel));

                    AddPermissionClaim(permission.ShowBrand, nameof(permission.ShowBrand));
                    AddPermissionClaim(permission.CreateBrand, nameof(permission.CreateBrand));
                    AddPermissionClaim(permission.UpdateBrand, nameof(permission.UpdateBrand));
                    AddPermissionClaim(permission.DeleteBrand, nameof(permission.DeleteBrand));

                    AddPermissionClaim(permission.ShowInstagram, nameof(permission.ShowInstagram));
                    AddPermissionClaim(permission.CreateInstagram, nameof(permission.CreateInstagram));
                    AddPermissionClaim(permission.UpdateInstagram, nameof(permission.UpdateInstagram));
                    AddPermissionClaim(permission.DeleteInstagram, nameof(permission.DeleteInstagram));

                    AddPermissionClaim(permission.ShowBlog, nameof(permission.ShowBlog));
                    AddPermissionClaim(permission.CreateBlog, nameof(permission.CreateBlog));
                    AddPermissionClaim(permission.UpdateBlog, nameof(permission.UpdateBlog));
                    AddPermissionClaim(permission.DeleteBlog, nameof(permission.DeleteBlog));

                    AddPermissionClaim(permission.ShowDestination, nameof(permission.ShowDestination));
                    AddPermissionClaim(permission.CreateDestination, nameof(permission.CreateDestination));
                    AddPermissionClaim(permission.UpdateDestination, nameof(permission.UpdateDestination));
                    AddPermissionClaim(permission.DeleteDestination, nameof(permission.DeleteDestination));

                    AddPermissionClaim(permission.ShowTeamMember, nameof(permission.ShowTeamMember));
                    AddPermissionClaim(permission.CreateTeamMember, nameof(permission.CreateTeamMember));
                    AddPermissionClaim(permission.UpdateTeamMember, nameof(permission.UpdateTeamMember));
                    AddPermissionClaim(permission.DeleteTeamMember, nameof(permission.DeleteTeamMember));

                    AddPermissionClaim(permission.ShowTrandingDestination, nameof(permission.ShowTrandingDestination));
                    AddPermissionClaim(permission.CreateTrandingDestination, nameof(permission.CreateTrandingDestination));
                    AddPermissionClaim(permission.UpdateTrandingDestination, nameof(permission.UpdateTrandingDestination));
                    AddPermissionClaim(permission.DeleteTrandingDestination, nameof(permission.DeleteTrandingDestination));

                    AddPermissionClaim(permission.ShowTour, nameof(permission.ShowTour));
                    AddPermissionClaim(permission.CreateTour, nameof(permission.CreateTour));
                    AddPermissionClaim(permission.UpdateTour, nameof(permission.UpdateTour));
                    AddPermissionClaim(permission.DeleteTour, nameof(permission.DeleteTour));

                    AddPermissionClaim(permission.ShowSlider, nameof(permission.ShowSlider));
                    AddPermissionClaim(permission.CreateSlider, nameof(permission.CreateSlider));
                    AddPermissionClaim(permission.UpdateSlider, nameof(permission.UpdateSlider));
                    AddPermissionClaim(permission.DeleteSlider, nameof(permission.DeleteSlider));

                    AddPermissionClaim(permission.ShowNewsletter, nameof(permission.ShowNewsletter));
                    AddPermissionClaim(permission.DeleteNewsletter, nameof(permission.DeleteNewsletter));

                    AddPermissionClaim(permission.ShowActivity, nameof(permission.ShowActivity));
                    AddPermissionClaim(permission.CreateActivity, nameof(permission.CreateActivity));
                    AddPermissionClaim(permission.UpdateActivity, nameof(permission.UpdateActivity));
                    AddPermissionClaim(permission.DeleteActivity, nameof(permission.DeleteActivity));

                    AddPermissionClaim(permission.ShowAmenity, nameof(permission.ShowAmenity));
                    AddPermissionClaim(permission.CreateAmenity, nameof(permission.CreateAmenity));
                    AddPermissionClaim(permission.UpdateAmenity, nameof(permission.UpdateAmenity));
                    AddPermissionClaim(permission.DeleteAmenity, nameof(permission.DeleteAmenity));

                    AddPermissionClaim(permission.ShowCountries, nameof(permission.ShowCountries));
                    AddPermissionClaim(permission.CreateCountries, nameof(permission.CreateCountries));
                    AddPermissionClaim(permission.UpdateCountries, nameof(permission.UpdateCountries));
                    AddPermissionClaim(permission.DeleteCountries, nameof(permission.DeleteCountries));

                    AddPermissionClaim(permission.ShowCities, nameof(permission.ShowCities));
                    AddPermissionClaim(permission.CreateCities, nameof(permission.CreateCities));
                    AddPermissionClaim(permission.UpdateCities, nameof(permission.UpdateCities));
                    AddPermissionClaim(permission.DeleteCities, nameof(permission.DeleteCities));

                    AddPermissionClaim(permission.ShowSliderInfo, nameof(permission.ShowSliderInfo));
                    AddPermissionClaim(permission.CreateSliderInfo, nameof(permission.CreateSliderInfo));
                    AddPermissionClaim(permission.UpdateSliderInfo, nameof(permission.UpdateSliderInfo));
                    AddPermissionClaim(permission.DeleteSliderInfo, nameof(permission.DeleteSliderInfo));

                    AddPermissionClaim(permission.ShowSpecialOffer, nameof(permission.ShowSpecialOffer));
                    AddPermissionClaim(permission.CreateSpecialOffer, nameof(permission.CreateSpecialOffer));
                    AddPermissionClaim(permission.UpdateSpecialOffer, nameof(permission.UpdateSpecialOffer));
                    AddPermissionClaim(permission.DeleteSpecialOffer, nameof(permission.DeleteSpecialOffer));

                    AddPermissionClaim(permission.ShowExperience, nameof(permission.ShowExperience));
                    AddPermissionClaim(permission.CreateExperience, nameof(permission.CreateExperience));
                    AddPermissionClaim(permission.UpdateExperience, nameof(permission.UpdateExperience));
                    AddPermissionClaim(permission.DeleteExperience, nameof(permission.DeleteExperience));

                    AddPermissionClaim(permission.ShowAboutAgency, nameof(permission.ShowAboutAgency));
                    AddPermissionClaim(permission.CreateAboutAgency, nameof(permission.CreateAboutAgency));
                    AddPermissionClaim(permission.UpdateAboutAgency, nameof(permission.UpdateAboutAgency));
                    AddPermissionClaim(permission.DeleteAboutAgency, nameof(permission.DeleteAboutAgency));

                    AddPermissionClaim(permission.ShowChooseUsAbout, nameof(permission.ShowChooseUsAbout));
                    AddPermissionClaim(permission.CreateChooseUsAbout, nameof(permission.CreateChooseUsAbout));
                    AddPermissionClaim(permission.UpdateChooseUsAbout, nameof(permission.UpdateChooseUsAbout));
                    AddPermissionClaim(permission.DeleteChooseUsAbout, nameof(permission.DeleteChooseUsAbout));

                    AddPermissionClaim(permission.ShowAboutTeam, nameof(permission.ShowAboutTeam));
                    AddPermissionClaim(permission.CreateAboutTeam, nameof(permission.CreateAboutTeam));
                    AddPermissionClaim(permission.UpdateAboutTeam, nameof(permission.UpdateAboutTeam));
                    AddPermissionClaim(permission.DeleteAboutTeam, nameof(permission.DeleteAboutTeam));

                    AddPermissionClaim(permission.ShowAboutTravil, nameof(permission.ShowAboutTravil));
                    AddPermissionClaim(permission.CreateAboutTravil, nameof(permission.CreateAboutTravil));
                    AddPermissionClaim(permission.UpdateAboutTravil, nameof(permission.UpdateAboutTravil));
                    AddPermissionClaim(permission.DeleteAboutTravil, nameof(permission.DeleteAboutTravil));

                    AddPermissionClaim(permission.ShowPlan, nameof(permission.ShowPlan));
                    AddPermissionClaim(permission.CreatePlan, nameof(permission.CreatePlan));
                    AddPermissionClaim(permission.UpdatePlan, nameof(permission.UpdatePlan));
                    AddPermissionClaim(permission.DeletePlan, nameof(permission.DeletePlan));

                    AddPermissionClaim(permission.ShowContact, nameof(permission.ShowContact));

                    AddPermissionClaim(permission.ShowReview, nameof(permission.ShowReview));
                    AddPermissionClaim(permission.DeleteReview, nameof(permission.DeleteReview));

                    AddPermissionClaim(permission.ShowUser, nameof(permission.ShowUser));
                    AddPermissionClaim(permission.CreateUser, nameof(permission.CreateUser));
                    AddPermissionClaim(permission.UpdateUser, nameof(permission.UpdateUser));
                    AddPermissionClaim(permission.DeleteUser, nameof(permission.DeleteUser));

                    AddPermissionClaim(permission.ShowBooking, nameof(permission.ShowBooking));
                    AddPermissionClaim(permission.UpdateBooking, nameof(permission.UpdateBooking));

                    AddPermissionClaim(permission.ShowSetting, nameof(permission.ShowSetting));
                    AddPermissionClaim(permission.UpdateSetting, nameof(permission.UpdateSetting));
                }


                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, loginResponse?.Error ?? "Login failed.");
                return View(model);
            }
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View(new Register());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var registerResponse = await _accountService.RegisterAsync(request);

            if (!registerResponse)
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
                return View(request);
            }
            return View("VerifyEmail"); 
        }
        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string verifyEmail, string token)
        {
            if (verifyEmail == null || token == null)
                return BadRequest("Invalid confirmation request.");

            var response = await _accountService.VerifyEmailAsync(verifyEmail, token);

            ViewBag.Message = response;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var userId = User.Identity.Name;
            //if (!string.IsNullOrEmpty(userId))
            //{
            //    await _basketService.ClearBasketAsync(userId);
            //}
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email cannot be empty.");
                return View();
            }

            var requestUri = "https://localhost:7145/api/Account/ForgetPassword";
            var response = await _httpClient.PostAsJsonAsync(requestUri, email);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObj = System.Text.Json.JsonSerializer.Deserialize<ResponseObject>(responseContent, options);
                TempData["Message"] = responseObj.ResponseMessage;
                return RedirectToAction("ForgetPasswordConfirmation");
            }   
            else
            {
                var responseObj = await response.Content.ReadFromJsonAsync<ResponseObject>();
                ModelState.AddModelError("", responseObj.ResponseMessage);
                return View();
            }
        }

        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            var model = new UserPasswordVM
            {
                email = email,
                token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserPasswordVM userPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userPasswordVM);
            }

            var requestUri = "https://localhost:7145/api/Account/ResetPassword";
            var response = await _httpClient.PostAsJsonAsync(requestUri, userPasswordVM);

            if (response.IsSuccessStatusCode)
            {
                var responseObj = await response.Content.ReadFromJsonAsync<ResponseObject>();
                TempData["Message"] = responseObj.ResponseMessage;
                return RedirectToAction("ResetPasswordConfirmation");
            }
            else
            {
                var responseObj = await response.Content.ReadFromJsonAsync<ResponseObject>(); 
                ModelState.AddModelError("", responseObj.ResponseMessage);
                return View(userPasswordVM);
            }
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7145/api/account/getprofile");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Profil məlumatları alınmadı.";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            var profile = System.Text.Json.JsonSerializer.Deserialize<ProfileViewModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Mövcud emaili Session-da saxla
            HttpContext.Session.SetString("CurrentEmail", profile.Email ?? "");

            return View(profile);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM model)
        {
            if (!ModelState.IsValid)
            {
                var invalidModel = new ProfileViewModel
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };
                return View("Profile", invalidModel);
            }

            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login");

            var currentEmail = HttpContext.Session.GetString("CurrentEmail");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7145/api/account/updateprofile", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = error;

                var errorModel = new ProfileViewModel
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };
                return View("Profile", errorModel);
            }

            // Mail dəyişibsə logout et
            if (!string.Equals(currentEmail, model.Email, StringComparison.OrdinalIgnoreCase))
            {
                await HttpContext.SignOutAsync("CookieAuth");
                HttpContext.Session.Remove("AuthToken");
                HttpContext.Session.Remove("CurrentEmail");

                TempData["InfoMessage"] = "Email dəyişdi, zəhmət olmasa yeni emailinizi təsdiqləyin.";
                return RedirectToAction("Login");
            }

            // Əks halda profile-a qaytar
            return RedirectToAction("Profile");
        }





    }
}
