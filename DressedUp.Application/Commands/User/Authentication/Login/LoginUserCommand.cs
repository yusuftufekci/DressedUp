using DressedUp.Application.DTOs.Authentication;
using DressedUp.Application.Responses;
using MediatR;

namespace DressedUp.Application.Commands.User.Authentication.Login;

public class LoginUserCommand : IRequest<Result<AuthData>>
{
    public string? Email { get; set; }
    public string Password { get; set; }
    public string? Username { get; set; }
}