using bootcamp.Service;
using Bootcamp.Repository;
using bootcamp.Service.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NetBootcamp.API.Extensions;
using NetBootcamp.API.Filters;
using System.Text;
using Bootcamp.Repository.Identities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x => x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService(builder.Configuration);


// add to Identity
// UserManager<AppUser> UserManager
// RoleManager<AppRole> RoleManager
// SignInManager<AppUser> SignInManager

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>()!;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidateIssuer = true,

        ValidAudiences = tokenOptions.Audience,
        ValidateAudience = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Signature)),
        ValidateIssuerSigningKey = true,

        ValidateLifetime = true
    };
});


var app = builder.Build();

app.SeedDatabase();

app.AddMiddlewares();

app.Run();