using Electric.API.Database;
using Electric.API.Middlewares;
using Electric.API.Services.Bills;
using Electric.API.Services.Meters;
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

builder.Services.AddTransient<IMeterService, MeterService>();
builder.Services.AddTransient<IBillService, BillService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();