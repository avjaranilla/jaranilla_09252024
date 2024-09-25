using jaranilla_09252024.application.Implementation;
using jaranilla_09252024.application.Implementation.Repositories;
using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.application.Interfaces.Services;
using jaranilla_09252024.infrastracture.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseInMemoryDatabase("PizzaDb"));


builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
