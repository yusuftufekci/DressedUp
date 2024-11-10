using DressedUp.Application.Common.Enums;
using DressedUp.Application.Common.Interfaces;
using DressedUp.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DressedUp.Application.Common.Services;

public class ClientIpService : IClientIpService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClientIpService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetClientIpAddress()
    {
        var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        // Proxy veya Load Balancer üzerinden geliyorsa gerçek IP'yi al
        if (_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Forwarded-For") == true)
        {
            ipAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        }
        if (string.IsNullOrEmpty(ipAddress))
        {
            throw new CustomException("Client IP address could not be determined.", ErrorCode.IpAddressNotFound);
        }
        return ipAddress ?? "IP Address Not Found";
    }
}