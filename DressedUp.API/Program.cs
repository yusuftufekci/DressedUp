using System.Reflection;
using DressedUp.API.Middleware;
using DressedUp.Application.Commands.User.Authentication;
using DressedUp.Application.Mappings;
using DressedUp.Application.Validators;
using DressedUp.Infrastructure.Data;
using DressedUp.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Validation
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>());

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
var infrastructureAssembly = Assembly.GetAssembly(typeof(UserRepository)); // Örneğin UserRepository gibi bilinen bir repository classını kullanabilirsiniz

builder.Services.Scan(scan => scan
    .FromAssemblies(infrastructureAssembly!)
    .AddClasses(classes => classes.Where(type => type.Namespace == "DressedUp.Infrastructure.Repositories"))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.AddControllers();

// AutoMapper konfigürasyonu
builder.Services.AddAutoMapper(cfg => MappingProfileRegister.RegisterMappings(cfg));

//Mediatr implementation
builder.Services.AddMediatR(typeof(RegisterUserCommand).Assembly);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // Swagger Annotations'ı etkinleştir
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
   // app.UseSwagger();
 //   app.UseSwaggerUI();
//}

 if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
 {
     app.UseSwagger();
     app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
         c.RoutePrefix = string.Empty; // Swagger'ı ana dizinde çalıştırmak için
     });
 }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();


app.MapControllers();

app.Run();
