using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Roomies.Application.Constants;
using Roomies.Application.Enums;
using Roomies.Application.Helpers;
using Roomies.Application.Interfaces;
using Roomies.Application.Interfaces.Repositories;
using Roomies.Application.Interfaces.Services;
using Roomies.Application.Models;
using Roomies.Domain.Entities;

namespace Roomies.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<RegisterUserResult, Enum>> RegisterUserAsync(
            RegisterUserDto registerUserDto)
        {
            var validationError = await ValidateUserAsync(registerUserDto);
            if (validationError != null)
            {
                return ServiceResponse<RegisterUserResult, Enum>.Failure(
                    validationError,
                    HttpStatusCode.BadRequest);
            }

            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(registerUserDto.Password);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(newUser);

            await _unitOfWork.SaveChangesAsync();

            // Assuming JWT token generation logic is implemented
            string jwtToken = GenerateJwtToken(newUser, registerUserDto.TokenSecret);

            var result = new RegisterUserResult
            {
                Token = jwtToken
            };

            return ServiceResponse<RegisterUserResult, Enum>.Success(result);
        }

        private async Task<Enum> ValidateUserAsync(
            RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return ErrorCodes.MissingUserData;
            }

            if (string.IsNullOrWhiteSpace(registerUserDto.Name)
                || registerUserDto.Name.Length > CommonConstants.MaxNameLength)
            {
                return ErrorCodes.InvalidNameFormat;
            }

            if (!ValidationHelper.IsValidEmail(registerUserDto.Email))
            {
                return ErrorCodes.InvalidEmailFormat;
            }

            if (!ValidationHelper.IsValidPassword(registerUserDto.Password))
            {
                return ErrorCodes.InvalidPasswordFormat;
            }

            if (await _userRepository.EmailExistsAsync(registerUserDto.Email))
            {
                return ErrorCodes.UserAlreadyExists;
            }

            return null;
        }

        private string GenerateJwtToken(User user, string tokenSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(tokenSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
