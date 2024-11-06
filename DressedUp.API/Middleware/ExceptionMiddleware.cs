using System.Net;
using System.Text.Json;
using DressedUp.Application.Responses;
using DressedUp.Domain.Exceptions;
using FluentValidation;

namespace DressedUp.API.Middleware;

public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                DomainException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = GenerateErrorResponse(exception);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonResponse = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(jsonResponse);
        }

        private static Result<string> GenerateErrorResponse(Exception exception)
        {
            return exception switch
            {
                ValidationException validationException => 
                    Result<string>.Failure("Validation failed", validationException.Errors.Select(e => e.ErrorMessage).ToList()),

                DomainException domainException => 
                    Result<string>.Failure(domainException.Message),

                _ => Result<string>.Failure("An unexpected error occurred", new List<string> { exception.Message })
            };
        }
    }