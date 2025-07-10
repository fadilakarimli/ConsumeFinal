using FinalProjectConsume;
using FinalProjectConsume.Helpers;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// **?lav? et: DistributedMemoryCache lazýmdýr sessiya üçün**
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied"; // bu yolu tapmaða çalýþýr
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin", "SuperAdmin"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IInstagramService, InstagramService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IDestinationFeatureService, DestinationFeatureService>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
builder.Services.AddScoped<ITrandingDestinationService, TrandingDestinationService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<INewsLetterService, NewsLetterService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ISliderInfoService, SliderInfoService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IAboutAgencyService, AboutAgencyService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IChooseUsAboutService, ChooseUsAboutService>();
builder.Services.AddScoped<IAboutTeamMemberService, AboutTeamMemberService>();
builder.Services.AddScoped<IAboutAppService, AboutAppService>();
builder.Services.AddScoped<IAboutDestinationService, AboutDestinationService>();
builder.Services.AddScoped<IAboutBlogService, AboutBlogService>();
builder.Services.AddScoped<IAboutTravilService, AboutTravilService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<INewService, NewService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<TokenHandler>();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
}).AddHttpMessageHandler<TokenHandler>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
});

app.UseHttpsRedirection();
// app.UseMiddleware<GlobalExceptionHandler>();
app.UseStaticFiles();

app.UseRouting();

// **Sessiya mütl?q UseRouting-d?n sonra, UseAuthentication-d?n ?vv?l olmalýdýr**
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
