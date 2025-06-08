using Roomies.Application.Models;

namespace Roomies.Application.Interfaces.Services;

public interface IUserService
{
    Task<ServiceResponse<RegisterUserResult, Enum>> RegisterUserAsync(
        RegisterUserDto registerUserDto);

    Task<ServiceResponse<LoginResponseDto, Enum>> AuthenticateUser(
        LoginRequestDto request);

    Task<ServiceResponse<UserDetailsDto, Enum>> GetUserDetailsByIdAsync(Guid userId);
}
