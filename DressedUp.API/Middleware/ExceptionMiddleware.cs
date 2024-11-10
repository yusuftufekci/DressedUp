using System.Net;
using System.Text.Json;
using DressedUp.Application.Common.Enums;
using DressedUp.Application.Exceptions;
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
        
        // Hata türüne göre uygun status code ve response döndürme
        var (statusCode, response) = GenerateErrorResponse(exception);
        
        context.Response.StatusCode = statusCode;
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonResponse = JsonSerializer.Serialize(response, options);

        return context.Response.WriteAsync(jsonResponse);
    }

    private static (int statusCode, Result<string> response) GenerateErrorResponse(Exception exception)
    {
        if (exception is CustomException customException)
        {
            // ErrorCode’a göre özel HTTP durum kodları
            var statusCode = customException.ErrorCode switch
            {
                ErrorCode.InvalidRefreshToken => (int)HttpStatusCode.Unauthorized,
                ErrorCode.UserNotFound => (int)HttpStatusCode.NotFound,
                ErrorCode.IpAddressNotFound => (int)HttpStatusCode.BadRequest,
                ErrorCode.UnexpectedError => (int)HttpStatusCode.InternalServerError,
                ErrorCode.UsernameExists => (int)HttpStatusCode.Conflict,
                ErrorCode.EmailExists => (int)HttpStatusCode.Conflict,
                ErrorCode.UserCredantialFailed => (int)HttpStatusCode.Unauthorized,
                ErrorCode.ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            return (
                statusCode,
                Result<string>.Failure(customException.Message, customException.ErrorCode)
            );
        }

        // Diğer beklenmeyen hatalar için genel durum
        return (
            (int)HttpStatusCode.InternalServerError,
            Result<string>.Failure("An unexpected error occurred", ErrorCode.UnexpectedError, new List<string> { exception.Message })
        );
    }
    }