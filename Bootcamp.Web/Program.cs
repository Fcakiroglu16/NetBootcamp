using Bootcamp.Web.TokenServices;
using Bootcamp.Web.Users;
using Bootcamp.Web.WeatherServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<TokenService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("Microservices")["BaseUrl"]!);
});
builder.Services.AddHttpClient<UserService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("Microservices")["BaseUrl"]!);
}).AddHttpMessageHandler<ClientCredentialTokenInterceptor>();

builder.Services.AddHttpClient<WeatherService>(options =>
    {
        options.BaseAddress = new Uri(builder.Configuration.GetSection("Microservices")["BaseUrl"]!);
    })
    .AddHttpMessageHandler<ClientCredentialTokenInterceptor>();


builder.Services.Configure<TokenOption>(builder.Configuration.GetSection("TokenOption"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ClientCredentialTokenInterceptor>();
builder.Services.AddMemoryCache();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme,
    opt =>
    {
        opt.AccessDeniedPath = "/Home/AccessDenied";
        opt.LoginPath = "/Home/SignIn";
        opt.Cookie.Name = "bootcamp.cookie";
        opt.ExpireTimeSpan = TimeSpan.FromDays(30);
    });


builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();