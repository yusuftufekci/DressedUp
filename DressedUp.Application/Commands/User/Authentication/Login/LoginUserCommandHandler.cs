using DressedUp.Application.Common.Enums;
using DressedUp.Application.Common.Interfaces;
using DressedUp.Application.DTOs.Authentication;
using DressedUp.Application.Exceptions;
using DressedUp.Application.Responses;
using DressedUp.Domain.Interfaces;
using MediatR;

namespace DressedUp.Application.Commands.User.Authentication.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthData>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IClientIpService _clientIpService;


    public LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository, IClientIpService clientIpService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _clientIpService = clientIpService;
    }

    public async Task<Result<AuthData>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindOneAsync(p => p.Email == request.Email || p.Username == request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new CustomException("Invalid credentials", ErrorCode.UserCredantialFailed);
        
        var existingRefreshTokens = await _refreshTokenRepository.WhereAsync(p=>p.UserId == user.UserId && p.DeviceId == request.DeviceId);

         string  userIp = _clientIpService.GetClientIpAddress();

        
        foreach (var token in existingRefreshTokens)
        {
            token.Revoke(userIp); // RevokedByIp gibi bilgileri güncelleyebilirsiniz
            _refreshTokenRepository.Update(token);
        }
  
        // Access ve refresh token oluştur
        var accessToken = _tokenService.GenerateAccessToken(user, userIp, request.DeviceId);
        var refreshToken = _tokenService.GenerateRefreshToken(user.UserId, userIp, request.DeviceId);
        await _refreshTokenRepository.AddAsync(refreshToken);

        var authData = new AuthData
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };

        return Result<AuthData>.Success(authData);
    }
}
