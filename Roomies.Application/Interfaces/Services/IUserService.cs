using Roomies.Application.Models;

namespace Roomies.Application.Interfaces.Services;

public interface IUserService
{
    Task<ServiceResponse<RegisterUserResult, Enum>> RegisterUserAsync(
        RegisterUserDto registerUserDto);
}
