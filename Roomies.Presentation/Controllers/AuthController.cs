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
        private readonly IUserService _userService;
        private readonly IValidator<RegisterUserRequest> _registerUserValidator;
        private readonly IValidator<LoginRequest> _loginRequestValidator;

        public AuthController(
            IUserService userService,
            IValidator<RegisterUserRequest> registerUserValidator,
            IValidator<LoginRequest> loginRequestValidator)
        {
            _userService = userService;
            _registerUserValidator = registerUserValidator;
            _loginRequestValidator = loginRequestValidator;
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
                request.ToRegisterUserDto());

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data.ToRegisterUserResponse());
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token upon successful login.
        /// </summary>
        /// <param name="request">The login request containing user credentials.</param>
        /// <returns>A response containing the JWT token if successful, or an error message if authentication fails.</returns>
        /// <response code="200">Login successful. Returns the JWT token.</response>
        /// <response code="401">Invalid credentials. Returns an error message.</response>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var validationResult = await _loginRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.AuthenticateUser(
                request.ToLoginRequestDto());

            if (!result.IsSuccess)
            {
                return Unauthorized(result.Error);
            }

            return Ok(new LoginResponse { Token = result.Data.Token });
        }
    }
}
