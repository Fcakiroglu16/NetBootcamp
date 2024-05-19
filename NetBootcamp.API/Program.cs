using bootcamp.Service;
using Bootcamp.Repository;
using bootcamp.Service.Users;
using NetBootcamp.API.Extensions;
using NetBootcamp.API.Filters;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x => x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService(builder.Configuration);
builder.Services.AddScoped<IAuthorizationHandler, OverAgeRequirementHandler>();


builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Over18AgePolicy", x => { x.AddRequirements(new OverAgeRequirement() { Age = 10 }); });


    x.AddPolicy("UpdatePolicy", y => { y.RequireClaim("update", "true"); });
});


var app = builder.Build();

app.SeedDatabase();
await app.SeedIdentityData();
app.AddMiddlewares();

app.Run();