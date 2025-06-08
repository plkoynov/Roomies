using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies.Application.Interfaces.Services;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves the details (Name and Email) of a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>
    /// A 200 OK response with the user details if found, or a 404 Not Found response if no user is found.
    /// </returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserDetails(Guid id)
    {
        var result = await _userService.GetUserDetailsByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Data);
    }
}