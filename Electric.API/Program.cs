using Electric.API.Database;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add database service
builder.Services.AddDbContext<ElectricDbContext>(options => 
    options.UseSqlite(builder.Configuration
    .GetConnectionString("DefaultConnection"))
);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

//builder.Services.AddTransient<INTERFAZ_SERVICIO, SERVICIO>

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();