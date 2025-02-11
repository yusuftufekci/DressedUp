using DressedUp.Application.DTOs.Authentication;
using DressedUp.Application.Responses;
using MediatR;

namespace DressedUp.Application.Commands.User.Authentication;

public class RegisterUserCommand : IRequest<Result<AuthData>>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string DeviceId { get; set; }
}