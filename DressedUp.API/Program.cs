using System.Reflection;
using DressedUp.Domain.Interfaces;
using DressedUp.Infrastructure.Data;
using DressedUp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
var infrastructureAssembly = Assembly.GetAssembly(typeof(UserRepository)); // Örneğin UserRepository gibi bilinen bir repository classını kullanabilirsiniz

builder.Services.Scan(scan => scan
    .FromAssemblies(infrastructureAssembly!)
    .AddClasses(classes => classes.Where(type => type.Namespace == "OutfitSharingApp.Infrastructure.Repositories"))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

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