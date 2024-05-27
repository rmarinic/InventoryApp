using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

// Add CORS services
services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

services.AddDbContext<InvDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("invConn"), mySqlOptions =>
{
    mySqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5, // Number of retry attempts
        maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retry attempts
        errorNumbersToAdd: null); // List of additional error numbers to consider transient
}));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS middleware
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
