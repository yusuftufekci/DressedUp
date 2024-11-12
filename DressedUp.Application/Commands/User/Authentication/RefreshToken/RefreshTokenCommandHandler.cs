using DressedUp.Application.Common.Enums;
using DressedUp.Application.Common.Interfaces;
using DressedUp.Application.DTOs.Authentication;
using DressedUp.Application.Exceptions;
using DressedUp.Application.Responses;
using DressedUp.Domain.Interfaces;
using MediatR;

namespace DressedUp.Application.Commands.User.Authentication.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthData>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IClientIpService _clientIpService;


    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IUserRepository userRepository, IClientIpService clientIpService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _clientIpService = clientIpService;
    }

    public async Task<Result<AuthData>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Refresh token'ı doğrula
        var existingRefreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
        
        if (existingRefreshToken == null || existingRefreshToken.IsRevoked || existingRefreshToken.IsUsed || existingRefreshToken.ExpiryDate <= DateTime.UtcNow)
        {
            throw new CustomException("Invalid or expired refresh token", ErrorCode.InvalidRefreshToken);
        }

        // Kullanıcı bilgilerini al
        var user = await _userRepository.GetByIdAsync(existingRefreshToken.UserId);
        if (user == null)
        {
            throw new CustomException("User not found", ErrorCode.UserNotFound);
        }

        string userIp = _clientIpService.GetClientIpAddress();

        
        // Eski token'ı iptal et ve yeni bir refresh token oluştur
        existingRefreshToken.Revoke(userIp); // Kullanıcı IP'sini sağlayabilirsiniz
        await _refreshTokenRepository.UpdateAsync(existingRefreshToken);

        var newAccessToken = _tokenService.GenerateAccessToken(user, userIp, request.DeviceId);
        var newRefreshToken = _tokenService.GenerateRefreshToken(user.UserId, userIp, request.DeviceId);
        await _refreshTokenRepository.AddAsync(newRefreshToken);

        var authData = new AuthData
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token
        };

        return Result<AuthData>.Success(authData);
    }
}
