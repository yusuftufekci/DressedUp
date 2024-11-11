using DressedUp.Application.DTOs.Authentication;
using DressedUp.Application.Responses;
using MediatR;

namespace DressedUp.Application.Commands.User.Authentication.RefreshToken;

public class RefreshTokenCommand : IRequest<Result<AuthData>>
{
    public string RefreshToken { get; set; }
    public string DeviceId { get; set; }
}
