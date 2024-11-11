using DressedUp.Application.Commands.User.Authentication;
using DressedUp.Application.Commands.User.Authentication.Login;
using DressedUp.Application.Commands.User.Authentication.RefreshToken;
using DressedUp.Application.Common.Enums;
using DressedUp.Application.DTOs.Authentication;
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
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">The unique ID of the user.</param>
    /// <returns>A Result containing the user details if found; otherwise, an error message.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Fetches user by ID",
        Description = "Retrieves user information for the specified ID. Returns a 404 error if the user is not found."
    )]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound(Result<string>.Failure("User not found",ErrorCode.EmailExists));
        }

        return Ok(Result<User>.Success(user, "User retrieved successfully"));
    }
    
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="command">The user registration details.</param>
    /// <returns>A Result containing the new user's ID if registration is successful; otherwise, an error message.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<AuthData>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [SwaggerOperation(
        Summary = "Registers a new user",
        Description = "Creates a new user account using the provided registration details. Checks if the email is already registered."
    )]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    /// <summary>
    /// Logs in a user with email and password.
    /// </summary>
    /// <param name="command">The login command containing email and password.</param>
    /// <returns>A Result containing the auth data if successful; otherwise, an error message.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<AuthData>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status401Unauthorized)]
    [SwaggerOperation(
        Summary = "Logs in a user",
        Description = "Authenticates a user with the provided email and password."
    )]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    /// <summary>
    /// Refreshes the access token using a valid refresh token.
    /// </summary>
    /// <param name="command">The refresh token command with the current refresh token.</param>
    /// <returns>A Result containing the new access token and refresh token if successful; otherwise, an error message.</returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(Result<AuthData>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Refreshes access token",
        Description = "Generates a new access token using the provided refresh token. Ensures the refresh token is valid and not expired."
    )]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
}