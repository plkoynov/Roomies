using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Roomies.Application.Interfaces.Services;
using Roomies.Presentation.Models;

namespace Roomies.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IValidator<RegisterUserRequest> _registerUserValidator;

        public AuthController(
            IConfiguration configuration,
            IUserService userService,
            IValidator<RegisterUserRequest> registerUserValidator)
        {
            _configuration = configuration;
            _userService = userService;
            _registerUserValidator = registerUserValidator;
        }

        /// <summary>
        /// Registers a new user and returns a JWT token upon successful registration.
        /// </summary>
        /// <param name="request">The registration request containing user details.</param>
        /// <returns>A response containing the JWT token if successful, or validation errors if the request is invalid.</returns>
        /// <response code="200">Registration successful. Returns the user ID and JWT token.</response>
        /// <response code="400">Invalid request. Returns validation errors.</response>
        [HttpPost("register")]
        public async Task<ActionResult<RegisterUserResponse>> Register([FromBody] RegisterUserRequest request)
        {
            var validationResult = await _registerUserValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.RegisterUserAsync(
                request.ToRegisterUserDto(_configuration["Jwt:TokenSecret"]));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data.ToRegisterUserResponse());
        }
    }
}
