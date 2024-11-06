using AutoMapper;
using DressedUp.Application.Responses;
using DressedUp.Domain.Interfaces;
using MediatR;

namespace DressedUp.Application.Commands.User.Authentication;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user already exists (optional)
        var existingUser = await _userRepository.FindOneAsync(p => p.Email == request.Email || p.Username == request.Username);
        if (existingUser != null)
        {
            return Result<int>.Failure("User with this email already exists.");
        }

        // Map RegisterUserCommand to User entity
        var user = _mapper.Map<Domain.Aggregates.UserAggregate.User>(request);
        try
        {
            await _userRepository.AddAsync(user);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            Console.WriteLine($"Error: {errorMessage}");
            throw;
        }
        // Add user to the database

        return Result<int>.Success(user.UserId, "User registered successfully");
    }
}