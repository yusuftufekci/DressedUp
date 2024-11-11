using AutoMapper;
using DressedUp.Application.Common.Enums;
using DressedUp.Application.Common.Interfaces;
using DressedUp.Application.DTOs.Authentication;
using DressedUp.Application.Exceptions;
using DressedUp.Application.Responses;
using DressedUp.Domain.Interfaces;
using MediatR;

namespace DressedUp.Application.Commands.User.Authentication;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthData>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IClientIpService _clientIpService;


    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IClientIpService clientIpService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _clientIpService = clientIpService;
    }

    public async Task<Result<AuthData>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FindOneAsync(p=> p.Email == request.Email || p.Username == request.Username);
        if (existingUser != null && existingUser.Email == request.Email)
            throw new CustomException("Email already exists", ErrorCode.EmailExists);
        if (existingUser != null && existingUser.Username == request.Username)
        {
            throw new CustomException("Username already exists", ErrorCode.UsernameExists);
        }
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new Domain.Aggregates.UserAggregate.User(request.Username, request.Name, request.Lastname, request.Email, passwordHash);
        await _userRepository.AddAsync(user);
        
        string userIp = _clientIpService.GetClientIpAddress();
        
        // Yeni access ve refresh token oluştur
        var accessToken = _tokenService.GenerateAccessToken(user,userIp);
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