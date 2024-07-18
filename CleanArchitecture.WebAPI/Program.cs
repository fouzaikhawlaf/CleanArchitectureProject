
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.Infrastructure.Data.Repository;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Register repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Register services
builder.Services.AddScoped<IClientService, ClientService>();



// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000", "https://localhost:3000", "https://127.0.0.1:3000")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();


app.MapControllers(); // Ensure controllers are mapped

app.Run();