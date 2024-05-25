using Bootcamp.Web.TokenServices;
using Bootcamp.Web.WeatherServices;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<TokenService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("Microservices")["BaseUrl"]!);
});


builder.Services.AddHttpClient<WeatherService>(options =>
    {
        options.BaseAddress = new Uri(builder.Configuration.GetSection("Microservices")["BaseUrl"]!);
    })
    .AddHttpMessageHandler<ClientCredentialTokenInterceptor>();


builder.Services.Configure<TokenOption>(builder.Configuration.GetSection("TokenOption"));


builder.Services.AddScoped<ClientCredentialTokenInterceptor>();
builder.Services.AddMemoryCache();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();