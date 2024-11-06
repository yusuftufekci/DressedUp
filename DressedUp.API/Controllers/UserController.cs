using DressedUp.Application.Commands.User.Authentication;
using DressedUp.Application.Responses;
using DressedUp.Domain.Aggregates.UserAggregate;
using DressedUp.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DressedUp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;


    public UserController(IMediator mediator, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _mediator = mediator;

    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>A Result<User> response.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<User>), 200)]
    [ProducesResponseType(typeof(Result<string>), 404)]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound(Result<string>.Failure("User not found"));
        }

        return Ok(Result<User>.Success(user, "User retrieved successfully"));
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Registers a new user",
        Description = "Creates a new user account with the provided information. Checks if the email is already registered."
    )]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    
}