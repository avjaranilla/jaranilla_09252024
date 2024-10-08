using jaranilla_09252024.application.Implementation.Repositories;
using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.application.Services.Implementation;
using jaranilla_09252024.application.Services.Interfaces;
using jaranilla_09252024.domain.Repositories;
using jaranilla_09252024.infrastracture.DBContext;
using jaranilla_09252024.infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PizzaDb")); //for using InMemory DB


// Dependency Injections
builder.Services.AddScoped<IPizzaRepositoryService, PizzaRepositoryService>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

builder.Services.AddScoped<IFileProcessingLogRepositoryService, FileProcessingLogRepositoryService>();
builder.Services.AddScoped<IFileProcessingLogRepository, FileProcessingLogRepository>();

builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<IJsonReaderService, JsonReaderService>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizza API", Version = "v1" });

    // Add API Key support
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "API Key needed to access the endpoints. Please enter the API Key in the format **X-API-Key**.",
        Name = "X-API-Key",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
