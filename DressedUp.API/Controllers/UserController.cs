using DressedUp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DressedUp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET api/user/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
            
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }
            
        return Ok(user);
    }
}