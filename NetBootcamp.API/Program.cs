using Bootcamp.Repository;
using NetBootcamp.API.Extensions;
using NetBootcamp.API.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x => x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService();

var app = builder.Build();

app.SeedDatabase();

app.AddMiddlewares();

app.Run();