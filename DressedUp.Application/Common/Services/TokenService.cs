using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DressedUp.Application.Common.Interfaces;
using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DressedUp.Application.Common.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(User user, string ipAddress)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),                 // User ID
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),                 // Username
                new Claim(JwtRegisteredClaimNames.Email, user.Email),                        // Email
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),           // Token ID
                new Claim("device", "User's device info"),                                   // Cihaz Bilgisi (Gerektiğinde değiştirin)
                new Claim("ip", ipAddress),                                        // IP Adresi (Güvenlik için kullanıcı IP adresi)
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtSettings:Issuer"]), // Issuer
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["JwtSettings:Audience"]) // Au
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:TokenExpirationMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(int userId, string ipAddress, string deviceId)
    {
        return new RefreshToken(userId, GenerateSecureToken(), DateTime.UtcNow.AddDays(7), ipAddress, deviceId);
    }

    private string GenerateSecureToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
